using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Queries.Aseguradoras
{
    public class InsurerQuery
    {
        public int IdSeguro { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public decimal? Cobertura { get; set; }
        public decimal? Prima { get; set; }
    }
}
