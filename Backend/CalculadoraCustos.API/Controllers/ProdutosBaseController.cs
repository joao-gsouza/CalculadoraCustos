using CalculadoraCustos.Application.DTOs.ProdutoBase;
using CalculadoraCustos.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculadoraCustos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosBaseController : ControllerBase
    {
        private readonly ProdutoBaseService _service;

        public ProdutosBaseController(ProdutoBaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            return Ok(await _service.ObterTodosAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var produto = await _service.ObterPorIdAsync(id);
            if (produto == null) return NotFound();
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] CriarProdutoBaseDto dto)
        {
            var produto = await _service.AdicionarAsync(dto);
            return CreatedAtAction(nameof(ObterPorId), new { id = produto.Id }, produto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarProdutoBaseDto dto)
        {
            await _service.AtualizarAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            await _service.RemoverAsync(id);
            return NoContent();
        }
    }
}
