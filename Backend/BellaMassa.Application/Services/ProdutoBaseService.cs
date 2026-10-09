using BellaMassa.Application.DTOs.ProdutoBase;
using BellaMassa.Domain.Interfaces;
using BellaMassa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.Services
{
    public class ProdutoBaseService
    {
        private readonly IProdutoBaseRepository _repository;

        public ProdutoBaseService(IProdutoBaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProdutoBaseDto>> ObterTodosAsync()
        {
            var produtos = await _repository.ObterTodosAsync();
            return produtos.Select(p => new ProdutoBaseDto(p.Id, p.Nome, p.PrecoEmbalagem, p.QuantidadeEmbalagem));
        }

        public async Task<ProdutoBaseDto?> ObterPorIdAsync(int id)
        {
            var produto = await _repository.ObterPorIdAsync(id);
            if (produto == null) return null;
            return new ProdutoBaseDto(produto.Id, produto.Nome, produto.PrecoEmbalagem, produto.QuantidadeEmbalagem);
        }

        public async Task<ProdutoBaseDto> AdicionarAsync(CriarProdutoBaseDto dto)
        {
            var produto = new ProdutoBase
            {
                Nome = dto.Nome,
                PrecoEmbalagem = dto.PrecoEmbalagem,
                QuantidadeEmbalagem = dto.QuantidadeEmbalagem
            };

            var result = await _repository.AdicionarAsync(produto);
            return new ProdutoBaseDto(result.Id, result.Nome, result.PrecoEmbalagem, result.QuantidadeEmbalagem);
        }

        public async Task AtualizarAsync(int id, AtualizarProdutoBaseDto dto)
        {
            var produto = await _repository.ObterPorIdAsync(id);
            if (produto == null) return;

            produto.Nome = dto.Nome;
            produto.PrecoEmbalagem = dto.PrecoEmbalagem;
            produto.QuantidadeEmbalagem = dto.QuantidadeEmbalagem;

            await _repository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(int id)
        {
            await _repository.RemoverAsync(id);
        }
    }
}
