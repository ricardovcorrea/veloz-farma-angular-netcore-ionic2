using IhChegou.Repository.Contract;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Session
{
    public class ElasticSession : IElasticSession
    {
        private const string SERVER = "http://10.1.32.21:8201/";

        public IElasticClient GetElasticClient()
        {
            var node = new Uri(SERVER);

            var settings = new ConnectionSettings(
                node
            );

            settings.DefaultIndex("ihchegou");

            return new ElasticClient(settings);
        }
    }
}
