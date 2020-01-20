using IhChegou.Global.Exceptions;
using IhChegou.Global.Extensions.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IhChegou.WebApi.Extensions
{
    public class Error
    {
        public string Message { get; set; }
        public int Code { get; set; }
#if DEBUG
#else
        [JsonIgnore]
#endif
        public string Info { get; set; }

        public Error(BadRequest badrequest)
        {
            Message = badrequest.Message;
            Code = 400;
        }

        public Error(UnauthorizedAccessException unauthorized)
        {
            Message = unauthorized.Message;
            Code = 401;
        }
        public Error(GenericError genericError)
        {
            Message = genericError.Message;
            Code = 400;
        }
    }
}