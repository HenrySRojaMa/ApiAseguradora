using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries.Clientes
{
    public class ClientQuery
    {
        public int IdCliente { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int? Edad { get; set; }
    }
}
