using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Global.Exceptions
{
    public class BadRequest : Exception
    {
        public new Exception InnerException  { get; private set; }
        public new string Message { get; private set; }
        public BadRequest(Exception ex)
        {
            InnerException = ex;
            Message = ex.Message;
        }
    }
}
