using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries.Contratos
{
    public class ContractQuery
    {
        public int IdContrato { get; set; }
        public int? IdCliente { get; set; }
        public int? IdSeguro { get; set; }
    }
}
