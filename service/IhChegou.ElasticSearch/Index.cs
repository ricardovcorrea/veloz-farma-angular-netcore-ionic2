using IhChegou.DTO.Search;
using IhChegou.Repository.Query;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.ElasticSearch
{
    public static class Index
    {
        public static List<string> Products()
        {
            var querys = new ProductQueries();

            var products = querys.GetAllSearchProducts();

            Console.WriteLine($"Setting {products.Count} products");
            var client = ElasticClientFactory.GetElasticClient();
            var erros = new ConcurrentBag<string>();

            Parallel.ForEach(products, (item) =>
             {
                 try
                 {
                 var index = client.Index(item, i => i
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

        public static void UpdateRanking(int id, int views)
        {
            var client = ElasticClientFactory.GetElasticClient();

            var response = client.Get<DtoSearch>(id);

            var result = response.Source;

            result.Views = views;

            var index = client.Index(result, i => i.Id(result.ProductId));

        }
    }
}
