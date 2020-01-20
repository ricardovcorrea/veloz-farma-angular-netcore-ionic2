using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Search
{
   public static class ElasticClientFactory
    {

        public static ElasticClient GetElasticClient()
        {
            var node = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(
                node
            );
            settings.DefaultIndex("ihchegou");

            var client = new ElasticClient(settings);

            return client;
        }
    }
}
