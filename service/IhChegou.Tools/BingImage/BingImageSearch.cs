using IhChegou.Global.Extensions.String;
using IhChegou.Tools.BingImage.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IhChegou.Tools.BingImage
{
    public static class BingImageSearch
    {
        public static List<string> GetImages(string query)
        {

            // Request parameters
            var queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["q"] = query;
            queryString["count"] = "10";
            queryString["offset"] = "0";
            queryString["mkt"] = "pt-br";

            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/images/search?" + queryString;
            var client = new RestClient(uri);

            var request = new RestRequest();
            // Request headers
            request.AddHeader("Ocp-Apim-Subscription-Key", "fd1e9f0eebc34cf1953c314044f125e6");


            var response = client.Execute<SearchResult>(request);

            return response.Data.value.Select(i => i.contentUrl).ToList();
        }
    }
}
