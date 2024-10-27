using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses.Contratos
{
    public class ContractResponse
    {
        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public List<Insurer> Aseguradoras { get; set; }
    }
    public class Insurer
    {
        public int IdSeguro { get; set; }
        public string? Nombre { get; set; }
    }
}
