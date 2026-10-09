using BellaMassa.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BellaMassa.Application.DTOs.Prato
{
    public class PratoFichaTecnicaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal CustoTotal { get; set; }
        public List<ItemFichaTecnicaDto> Ingredientes { get; set; }
    }
}
