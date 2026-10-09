using System;
using System.Collections.Generic;
using System.Text;

namespace CalculadoraCustos.Application.DTOs.Prato
{
    public class ItemFichaTecnicaDto
    {
        public int Id { get; set; }
        public string NomeIngrediente { get; set; }
        public double Quantidade { get; set; }
        public decimal CustoFracionado { get; set; }
    }
}
