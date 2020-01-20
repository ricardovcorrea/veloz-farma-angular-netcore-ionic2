using HtmlAgilityPack;
using IhChegou.DTO.Product;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace IhChegou.Crawler.Medicines
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Init DB Session");
            Console.ForegroundColor = ConsoleColor.Gray;
            new IhChegou.Repository.Query.ProductQueries();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Searching...");
            Console.ForegroundColor = ConsoleColor.White;

            const string URL_BASE = "https://consultaremedios.com.br";
            const string URL_LISTA_MEDICAMENTOS = URL_BASE + "/medicamentos";

            var errorUrl = new ConcurrentBag<string>();


            var client = new WebClient();
            client.Encoding = Encoding.UTF8;

            var response = client.DownloadString(URL_LISTA_MEDICAMENTOS);

            var document = new HtmlDocument();
            document.LoadHtml(response);

            var letters = document.DocumentNode.SelectNodes("//*[@id=\"letras\"]/div/div/div/div/div[2]/*").Select(i => i.GetAttributeValue("href", ""));

            var prodUrls = new List<string>();
            foreach (var item in letters)
            {
                var prodUrl = new List<string>();
                int page = 1;
                do
                {
                    response = client.DownloadString(URL_BASE + item + "?pagina=" + page);
                    document = new HtmlDocument();
                    document.LoadHtml(response);
                    prodUrl = document.DocumentNode.SelectNodes("//*[@class=\"product-block__title\"]/a")?.Select(i => i.GetAttributeValue("href", ""))?.ToList();
                    if (prodUrl != null)
                        prodUrls.AddRange(prodUrl);
                    page++;

                } while (prodUrl != null && prodUrl?.Count() != 0);

            }

            //  var nodes = documents[0].DocumentNode.SelectNodes("//div[@class='item col-xs-12 col-sm-4 col-md-3']");


            // Parallel.ForEach(nodes, new ParallelOptions { MaxDegreeOfParallelism = 1 }, (nod) =>
            foreach (var prodUrl in prodUrls)
            {

                var ProdResponse = client.DownloadString(URL_BASE + prodUrl);
                var productDocument = new HtmlDocument();
                productDocument.LoadHtml(ProdResponse);


                var query = new IhChegou.Repository.Query.ProductQueries();

                var product = new DtoProduct();
                product.Name = HttpUtility.HtmlDecode(productDocument.DocumentNode.SelectSingleNode("//*[@class=\"product-header__title\"]").InnerText);

                try
                {

                    product.Serving = HttpUtility.HtmlDecode(productDocument.DocumentNode.SelectSingleNode("//*[@id=\"indication-collapse\"]").InnerText);



                    var producerName = productDocument.DocumentNode.SelectSingleNode("//*[@class=\"cr-icon-factory product-block__meta-icon\"]/..").InnerText;

                    var producer = query.GetAllProducers().Where(i => i.Name == producerName).SingleOrDefault();
                    if (producer == null)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("New Producer - " + producerName);
                        producer = new DtoProducer() { Name = producerName };
                        query.Save(ref producer);
                    }
                    product.Producer = producer;

                    var dbProduct = query.GetProductByNameAndProducer(product.Name, product.Producer);
                    if (dbProduct != null)
                        product = dbProduct;
                    else
                        Console.WriteLine("New Product - " + product.Name);

                    var skuUrls = productDocument.DocumentNode.SelectNodes("//*[@class=\"presentation-offer-info__description\"]/a")?.Select(i => i.GetAttributeValue("href", ""))?.ToList();

                    foreach (var skuUrl in skuUrls)
                    {

                        var skuResponse = client.DownloadString(URL_BASE + skuUrl);
                        var skutDocument = new HtmlDocument();
                        skutDocument.LoadHtml(skuResponse);

                        var infoNodes = skutDocument.DocumentNode.SelectNodes("//*[@class=\"extra-infos-block\"]");

                        if (skuUrls.IndexOf(skuUrl) == 0)
                        {
                            foreach (var infoNode in infoNodes)
                            {

                                switch (infoNode.FirstChild.InnerText)
                                {
                                    case "Tipo do Medicamento":
                                        if (infoNode.LastChild.InnerText == "Referência")
                                        {
                                            product.Type = Global.Enumerators.ProductType.Reference;
                                        }
                                        if (infoNode.LastChild.InnerText == "Similar")
                                        {
                                            product.Type = Global.Enumerators.ProductType.Similar;
                                        }
                                        if (infoNode.LastChild.InnerText == "Genérico")
                                        {
                                            product.Type = Global.Enumerators.ProductType.Generic;
                                        }
                                        break;
                                    case "Necessita de Receita":
                                        if (infoNode.SelectSingleNode("//*/b").InnerText == "Sim")
                                        {
                                            product.NeedRecipe = true;
                                        }
                                        break;
                                    case "Princípio Ativo":
                                        {
                                            var prodPrinciples = new List<DtoPrinciple>();

                                            var princepleText = infoNode.LastChild.InnerText.Split('+');

                                            if (princepleText.Length > 0)
                                            {
                                                foreach (var item in princepleText)
                                                {
                                                    var principle = query.GetAllPrnciples().Where(i => i.Name == item).SingleOrDefault();

                                                    if (principle == null)
                                                    {
                                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                        Console.WriteLine("New Principle - " + item);
                                                        principle = new DtoPrinciple() { Name = item };
                                                        query.Save(ref principle);
                                                    }
                                                    prodPrinciples.Add(principle);
                                                }
                                                foreach (var principle in prodPrinciples)
                                                {
                                                    product.Principles = product.Principles ?? new List<DtoPrinciple>();
                                                    if (product.Principles.FirstOrDefault(i => i.Id == principle.Id) == null)
                                                    {
                                                        product.Principles.Add(principle);
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            var categorias = productDocument.DocumentNode.SelectNodes("//*[@id='product-page']/div[1]/div/div[1]/div/div/nav/ul/li").Select(i => i.InnerText.Replace("\n", "")).ToList();

                            categorias.RemoveAt(categorias.IndexOf(categorias.FirstOrDefault()));
                            categorias.RemoveAt(categorias.IndexOf(categorias.LastOrDefault()));

                            var ProdCategories = new List<DtoCategory>();
                            foreach (var item in categorias)
                            {
                                var category = query.GetCategory(item);

                                if (category == null)
                                {

                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("New Category - " + item);

                                    var position = categorias.IndexOf(item);
                                    category = new DtoCategory() { Name = item };
                                    if (position > 0)
                                    {
                                        var father = query.GetCategory(categorias[position - 1]);
                                        father.SubCategories = father.SubCategories ?? new List<DtoCategory>();
                                        father.SubCategories.Add(category);
                                        category = father;
                                    }
                                    query.Save(ref category);
                                }
                                category = query.GetCategory(item);
                                ProdCategories.Add(category);
                            }
                            foreach (var category in ProdCategories)
                            {
                                product.Categories = new List<DtoCategory>();
                                product.Categories.Add(query.GetCategory(category.Name));
                            }
                        }

                        var newSku = new DtoSku
                        {
                            Image = skutDocument.DocumentNode.SelectSingleNode("//*[@class=\"product-header__beauty-image\"]/img")?.GetAttributeValue("src", ""),
                            Name = skutDocument.DocumentNode.SelectSingleNode("//*[@id=\"product-page\"]/div[1]/div/div[2]/div[2]/h1").InnerText,
                        };
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        var dbsku = product.Skus?.FirstOrDefault(i => i.Name == newSku.Name);
                        if (dbsku == null)
                        {
                            Console.WriteLine("New Sku - " + newSku.Name);
                        }
                        else
                            newSku.Id = dbsku.Id;
                        product.Skus = product.Skus ?? new List<DtoSku>();
                        if (product.Skus.Where(i => i.Id == newSku.Id).Count() > 0)
                            product.Skus.Remove(product.Skus.Where(i => i.Id == newSku.Id).SingleOrDefault());
                        product.Skus.Add(newSku);
                    }
                    Console.ForegroundColor = ConsoleColor.Green;

                    query.Save(ref product);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                     Console.WriteLine($"- {ex.Message} -{ ex.InnerException?.Message}");
                    Console.ResetColor();
                }

            }
            //});

            var writer = new StreamWriter("errorList.json");

            writer.WriteLine(JsonConvert.SerializeObject(errorUrl));

            writer.Close();
        }

    }

}



