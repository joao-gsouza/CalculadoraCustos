using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Application.DTOs.ProdutoBase
{
    public record ProdutoBaseDto(int Id, string Nome, decimal PrecoEmbalagem, double QuantidadeEmbalagem);
}
