using BellaMassa.Application.DTOs.Prato;
using BellaMassa.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BellaMassa.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PratosController : ControllerBase
    {
        PratoService _pratoService;
        public PratosController(PratoService pratoService) 
        {
            _pratoService = pratoService;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _pratoService.ObterTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var prato = await _pratoService.ObterPorIdAsync(id);
            if (prato == null) return NotFound();
            return Ok(prato);
        }

        [HttpGet("{id}/ficha-tecnica")]
        public async Task<IActionResult> ObterFichaTecnica(int id) 
        {
            var ficha = await _pratoService.ObterResumoFichaTecnicaAsync(id);

            if (ficha == null)
            {
                return NotFound(new { Mensage = "Prato não encontrado." });
            }

            return Ok(ficha);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CriarPratoDto dto)
        {
            var prato = await _pratoService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = prato.Id }, prato);
        }

        [HttpPost("{id}/ingredientes")]
        public async Task<IActionResult> AdicionarIngrediente(int id, [FromBody] AdicionarIngredienteDto dto)
        {
            var resultado = await _pratoService.AdicionarIngredienteAsync(id, dto);

            if (resultado == null)
                return NotFound(new { Mensagem = "Prato não encontrado." });

            return Ok(new { Mensagem = "Ingrediente adicionado à ficha técnica com sucesso!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarPratoDto dto)
        {
            await _pratoService.AtualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _pratoService.RemoverAsync(id);
            return NoContent();
        }

        [HttpDelete("{id}/ingredientes/{produtoBaseId}")]
        public async Task<IActionResult> RemoverIngrediente(int id, int produtoBaseId)
        {
            var sucesso = await _pratoService.RemoverIngredienteAsync(id, produtoBaseId);

            if (!sucesso)
                return NotFound(new { Mensagem = "Ingrediente não encontrado na ficha técnica deste prato." });

            return NoContent();
        }
    }
}
