using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Domain.Models
{
    public class Prato
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        public ICollection<ItemFichaTecnica> Ingredientes { get; set; } = new List<ItemFichaTecnica>();
    }
}
