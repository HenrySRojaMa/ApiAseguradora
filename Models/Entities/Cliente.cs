using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Contratos = new HashSet<Contrato>();
        }

        public int IdCliente { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int? Edad { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Contrato> Contratos { get; set; }
    }
}
