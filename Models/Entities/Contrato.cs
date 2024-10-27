using System;
using System.Collections.Generic;

namespace Models.Entities
{
    public partial class Contrato
    {
        public int IdContrato { get; set; }
        public int? IdCliente { get; set; }
        public int? IdSeguro { get; set; }
        public string? Estado { get; set; }

        public virtual Cliente? IdClienteNavigation { get; set; }
        public virtual Aseguradora? IdSeguroNavigation { get; set; }
    }
}
