using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.DTOs.ProdutoBase
{
    public record AtualizarProdutoBaseDto(string Nome, decimal PrecoEmbalagem, double QuantidadeEmbalagem);
}
