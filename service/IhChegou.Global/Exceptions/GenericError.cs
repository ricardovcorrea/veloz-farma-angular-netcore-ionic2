using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Global.Exceptions
{
    public class GenericError : Exception
    {
        public new string Message { get; private set; }

        public GenericError(string message)
        {
            Message = message;
        }
    }
}
