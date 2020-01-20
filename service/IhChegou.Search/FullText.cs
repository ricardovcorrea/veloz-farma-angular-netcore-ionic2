using IhChegou.DTO.Search;
using IhChegou.Global.Extensions.Object;
using IhChegou.Search.Model;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Search
{
    class FullText
    {
        public List<DtoSearch> Get()
        {

            var client = ElasticClientFactory.GetElasticClient();
        

            var result = client.Search<DtoSearch>(s => s
            .From(0)
            .Size(1000)
            //.Query(q => q
            //    .MatchPhrasePrefix (mp => mp
            //        .Field(f1 => f1.Product)
            //        .Query("dor de cabeça")

            .Query(q => q				// define query
                .MultiMatch(mp => mp			// of type MultiMatch
                    .Query("dor de cabe")
                    .Type(TextQueryType.PhrasePrefix)// pass text
                    .Fields(f => f			// define fields to search against
                    .Fields(f1 => f1.Product,
                                  f2 => f2.Sku,
                                  f3 => f3.Serving,
                                  f4 => f4.Categories,
                                  f5 => f5.Principles,
                                  f6 => f6.Producer)))))
            ;

            var resultObj = result.Documents;

            Console.Write(resultObj.ToJson());
            Console.ReadKey();


        }
    }
}
