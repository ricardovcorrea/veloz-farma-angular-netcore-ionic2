using IhChegou.DTO.Search;
using IhChegou.Repository.Pharma.Database;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
using IhChegou.Repository.Contract;

namespace IhChegou.Repository.Pharma.Elastic
{
    public class SearchRepository : ISearchRepository
    {

        public IProductRepository ProductQueries { get; set; }
        public IElasticClient ElasticClient { get; set; }

        public SearchRepository(IProductRepository productQueries, IElasticSession elasticClient)
        {
            ProductQueries = productQueries;
            ElasticClient = elasticClient.GetElasticClient();
        }

        public DtoSearchResult Fulltext(DtoSearchRequest request)
        {

            var query = @"{
                                ""function_score"": { 
                                  ""query"": { 
                                    ""multi_match"": {
                                      ""query"":    """+(request.Query)+@""",
                                      ""fields"": [ ""product"", ""serving"" ]
                                    }
                                  },
                                 ""field_value_factor"": {
                                    ""field"":    ""views"",
                                    ""modifier"": ""log1p"",
                                    ""factor"":   0.1
                                  },
                                  ""boost_mode"": ""sum"" 
                                }
                              }";

            var elasticQuery = new SearchRequest
            {
                Size = request.Take,
                From = request.From,
                Query = new RawQuery(query),
            };


            var result = ElasticClient.Search<DtoSearch>(elasticQuery);


            var resultObj = result.Documents;
            return new DtoSearchResult()
            {
                Result = resultObj?.ToList(),
                Request = request,
                Total = result.Total
                
            };
        }



        public void UpdateRanking(int id, int views)
        {


            var response = ElasticClient.Get<DtoSearch>(id);

            var result = response.Source;

            result.Views = views;

            var index = ElasticClient.Index(result, i => i.Id(result.ProductId));

        }

        public List<string> IndexAllProducts()
        {

            var products = ProductQueries.GetAllSearchProducts();

            Console.WriteLine($"Setting {products.Count} products");
            var erros = new ConcurrentBag<string>();

            Parallel.ForEach(products, (item) =>
            {
                try
                {
                    var index = ElasticClient.Index(item, i => i
                                                       .Id(item.SkuId));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro on Set Sku :" + item.SkuId);
                    Console.WriteLine(ex.Message);
                    erros.Add(ex.Message);
                    throw ex;
                }

            }
            );
            return erros.ToList();
        }


    }
}
