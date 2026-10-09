using BellaMassa.Application.Services;
using BellaMassa.Domain.Interfaces;
using BellaMassa.Domain.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Tests.Services
{
    public class PratoServiceTests
    {
        private readonly IPratoRepository _repository = Substitute.For<IPratoRepository>();
        private readonly PratoService _service;

        public PratoServiceTests()
        {
            _service = new PratoService(_repository);
        }

        [Fact]
        public async Task ObterResumoFichaTecnica_DeveCalcularCustoTotal_QuandoPratoTemIngredientes()
        {
            var farinha = new ProdutoBase { Id = 1, Nome = "Farinha", PrecoEmbalagem = 10m, QuantidadeEmbalagem = 1000 };
            var queijo = new ProdutoBase { Id = 2, Nome = "Queijo", PrecoEmbalagem = 30m, QuantidadeEmbalagem = 500 };

            var prato = new Prato
            {
                Id = 1,
                Nome = "Pizza",
                Ingredientes =
            {
                new ItemFichaTecnica { Id = 1, ProdutoBase = farinha, QuantidadeUtilizada = 250 },
                new ItemFichaTecnica { Id = 2, ProdutoBase = queijo,  QuantidadeUtilizada = 100 }
            }
            };

            _repository.ObterPratoComIngredientesAsync(1).Returns(prato);

            var resultado = await _service.ObterResumoFichaTecnicaAsync(1);
            Assert.NotNull(resultado);
            Assert.Equal(8.50m, resultado.CustoTotal);
            Assert.Equal(2, resultado.Ingredientes.Count);
            Assert.Equal(2.50m, resultado.Ingredientes[0].CustoFracionado);
        }

    }
}
