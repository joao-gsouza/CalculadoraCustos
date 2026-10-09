using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Domain.Models
{
    public class ProdutoBase
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoEmbalagem { get; set; }
        public double QuantidadeEmbalagem { get; set; }
    }
}
