using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.DTOs.Prato
{
    public record AdicionarIngredienteDto(int ProdutoBaseId, double QuantidadeUtilizada);
}
