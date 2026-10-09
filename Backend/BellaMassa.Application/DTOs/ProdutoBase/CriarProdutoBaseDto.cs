using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.DTOs.ProdutoBase
{
    public record CriarProdutoBaseDto(string Nome, decimal PrecoEmbalagem, double QuantidadeEmbalagem);
}
