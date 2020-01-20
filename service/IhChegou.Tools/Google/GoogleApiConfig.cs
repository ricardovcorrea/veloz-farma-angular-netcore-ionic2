using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Tools.Google
{
    public class GoogleApiConfig
    {
        protected string ApiKey { get; private set; }
        public GoogleApiConfig(string apikey)
        {
            ApiKey = apikey;
        }
    }
}
