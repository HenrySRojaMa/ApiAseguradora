using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Responses
{
    public static class RspHandler
    {
        public static Response OkQuery()
        {
            return new() { Code = MsgCd.Ok, Message = MsgCd.ValidQuery };
        }
        public static Response OkTransaction()
        {
            return new() { Code = MsgCd.Ok, Message = MsgCd.ValidTransaction };
        }
        public static Response BadResponse(Exception ex, string CurrentClass, string CurrentMethod)
        {
            return new() { Code = MsgCd.Error, Message = CurrentClass + " / " + CurrentMethod + " / " + ex.Message };
        }

    }
}
