using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Aseguradora
    {
        public Aseguradora()
        {
            Contratos = new HashSet<Contrato>();
        }

        public int IdSeguro { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public decimal? Cobertura { get; set; }
        public decimal? Prima { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
