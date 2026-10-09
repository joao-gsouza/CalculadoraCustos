using BellaMassa.Application.DTOs.Prato;
using BellaMassa.Domain.Interfaces;
using BellaMassa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.Services
{
    public class PratoService
    {
        IPratoRepository _pratoRepository;

        public PratoService(IPratoRepository pratoRepository) 
        {
            _pratoRepository = pratoRepository;
        }

        public async Task<IEnumerable<PratoDto>> ObterTodosAsync()
        {
            var pratos = await _pratoRepository.ObterTodosAsync();
            return pratos.Select(p => new PratoDto(p.Id, p.Nome));
        }

        public async Task<PratoDto?> ObterPorIdAsync(int id)
        {
            var prato = await _pratoRepository.ObterPorIdAsync(id);
            if (prato == null) return null;
            return new PratoDto(prato.Id, prato.Nome);
        }

        public async Task<PratoFichaTecnicaDto?> ObterResumoFichaTecnicaAsync(int pratoId)
        {
            var prato = await _pratoRepository.ObterPratoComIngredientesAsync(pratoId);

            if (prato == null) return null;

            var ingredientesDto = prato.Ingredientes.Select(i =>
            {
                var custoFracionado = (decimal)i.QuantidadeUtilizada * (i.ProdutoBase.PrecoEmbalagem / (decimal)i.ProdutoBase.QuantidadeEmbalagem);

                return new ItemFichaTecnicaDto()
                {
                    Id = i.Id,
                    NomeIngrediente = i.ProdutoBase.Nome,
                    Quantidade = i.QuantidadeUtilizada,
                    CustoFracionado = Math.Round(custoFracionado, 2)
                };
            }).ToList();

            var custoTotal = ingredientesDto.Sum(i => i.CustoFracionado);

            return new PratoFichaTecnicaDto() 
            {
                Id = prato.Id,
                Nome = prato.Nome,
                CustoTotal = custoTotal,
                Ingredientes = ingredientesDto,
            };
        }

        public async Task<PratoDto> AdicionarAsync(CriarPratoDto dto)
        {
            var prato = new Domain.Models.Prato { Nome = dto.Nome };

            await _pratoRepository.AdicionarAsync(prato);

            return new PratoDto(prato.Id, prato.Nome);
        }

        public async Task AtualizarAsync(int id, AtualizarPratoDto dto)
        {
            var prato = await _pratoRepository.ObterPorIdAsync(id);
            if (prato == null) return;

            prato.Nome = dto.Nome;
            await _pratoRepository.AtualizarAsync(prato);
        }

        public async Task RemoverAsync(int id)
        {
            await _pratoRepository.RemoverAsync(id);
        }

        public async Task<ItemFichaTecnicaDto?> AdicionarIngredienteAsync(int pratoId, AdicionarIngredienteDto dto)
        {
            var prato = await _pratoRepository.ObterPorIdAsync(pratoId);
            if (prato == null) return null;

            var novoItem = new ItemFichaTecnica
            {
                PratoId = pratoId,
                ProdutoBaseId = dto.ProdutoBaseId,
                QuantidadeUtilizada = dto.QuantidadeUtilizada
            };

            var result = await _pratoRepository.AdicionarIngredienteAsync(novoItem);

            return new ItemFichaTecnicaDto() 
            {
                Id = result.Id,
                NomeIngrediente = "",
                Quantidade = result.QuantidadeUtilizada,
                CustoFracionado = 0
            };
        }

        public async Task<bool> RemoverIngredienteAsync(int pratoId, int produtoBaseId)
        {
            var item = await _pratoRepository.ObterIngredienteAsync(pratoId, produtoBaseId);

            if (item == null) return false;

            await _pratoRepository.RemoverIngredienteAsync(item);
            return true;
        }
    }
}
