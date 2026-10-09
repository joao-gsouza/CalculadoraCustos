using BellaMassa.Application.DTOs.ProdutoBase;
using BellaMassa.Application.Services;
using BellaMassa.Domain.Interfaces;
using BellaMassa.Domain.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Tests.Services
{
    public class ProdutoBaseServiceTests
    {
        private readonly IProdutoBaseRepository _repository = Substitute.For<IProdutoBaseRepository>();
        private readonly ProdutoBaseService _service;

        public ProdutoBaseServiceTests()
        {
            _service = new ProdutoBaseService(_repository);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarDtoComDadosDoProduto_QuandoProdutoExiste()
        {
            var produto = new ProdutoBase { Id = 1, Nome = "Farinha", PrecoEmbalagem = 10m, QuantidadeEmbalagem = 1000 };
            _repository.ObterPorIdAsync(1).Returns(produto);

            var resultado = await _service.ObterPorIdAsync(1);
            Assert.NotNull(resultado);
            Assert.Equal(new ProdutoBaseDto(1, "Farinha", 10m, 1000), resultado);
        }

        [Fact]
        public async Task ObterPorId_DeveRetornarNull_QuandoProdutoNaoExiste()
        {
            _repository.ObterPorIdAsync(99).Returns((ProdutoBase?)null);

            var resultado = await _service.ObterPorIdAsync(99);
            Assert.Null(resultado);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarDtoComIdGerado_QuandoDadosValidos()
        {
            var dto = new CriarProdutoBaseDto("Queijo", 30m, 500);

            _repository.AdicionarAsync(Arg.Any<ProdutoBase>())
                .Returns(chamada =>
                {
                    var produto = chamada.Arg<ProdutoBase>();
                    produto.Id = 10;
                    return produto;
                });

            var resultado = await _service.AdicionarAsync(dto);
            Assert.Equal(10, resultado.Id);
            Assert.Equal("Queijo", resultado.Nome);

            await _repository.Received(1).AdicionarAsync(Arg.Is<ProdutoBase>(p =>
                p.Nome == "Queijo" &&
                p.PrecoEmbalagem == 30m &&
                p.QuantidadeEmbalagem == 500));
        }

        [Fact]
        public async Task Atualizar_NaoDeveChamarRepositorio_QuandoProdutoNaoExiste()
        {
            _repository.ObterPorIdAsync(99).Returns((ProdutoBase?)null);

            await _service.AtualizarAsync(99, new AtualizarProdutoBaseDto("X", 1m, 1));

            await _repository.DidNotReceive().AtualizarAsync(Arg.Any<ProdutoBase>());
        }
    }
}
