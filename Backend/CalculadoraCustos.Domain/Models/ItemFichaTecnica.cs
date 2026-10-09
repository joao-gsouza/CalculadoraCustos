using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Domain.Models
{
    public class ItemFichaTecnica
    {
        public int Id { get; set; }

        public int PratoId { get; set; }
        public Prato Prato { get; set; }
        public int ProdutoBaseId { get; set; }
        public ProdutoBase ProdutoBase { get; set; }
        public double QuantidadeUtilizada { get; set; }
    }
}
