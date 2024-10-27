using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public class Response
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public static class MsgCd
    {
        public static string Ok = "00";
        public static string Error = "01";

        public static string ValidQuery = "Consulta realizada con éxito";
        public static string BadQuery = "Error al consultar";
        public static string ValidTransaction = "Transaccion realizada con éxito";
        public static string BadTransaction = "Transaccion fallida";
    }
}
