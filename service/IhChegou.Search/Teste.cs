using IhChegou.Global.Extensions.Object;
using IhChegou.Repository.Model;
using IhChegou.Repository.Session;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Search
{
    public class Teste
    {
        public class Person
        {
            public string Id { get; set; }
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }
        public void SetToElastic()
        {

            var sessionManager = new SessionManager();

            SessionManager.Init();

            var session = sessionManager.NewSession();

            var prod = session.QueryOver<Repository.Model.Products.Search>().List();
            var client = ElasticClientFactory.GetElasticClient();

            var cont = 0;
            Parallel.ForEach(prod, (item) =>
            {
                var index = client.Index(item, i => i
                                                    .Id(item.ProductId));

                Console.WriteLine(cont);

                cont++;
            });

        }

        
    }
}
