using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.ElasticSearch
{
    static class ElasticClientFactory
    {
        private const string SERVER = "http://10.1.32.21:8201/";

        private static ElasticClient Client { get; set; }
        internal static ElasticClient GetElasticClient()
        {
            if (Client == null)
            {
                var node = new Uri(SERVER);

                var settings = new ConnectionSettings(
                    node
                );

                settings.DefaultIndex("ihchegou");

                Client = new ElasticClient(settings);
            }

            return Client;
        }
    }
}
