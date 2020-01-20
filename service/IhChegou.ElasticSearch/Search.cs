using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using IhChegou.Global.Extensions.Object;
using IhChegou.DTO.Search;

namespace IhChegou.ElasticSearch
{
    public static class Search
    {
        public static DtoSearchResult Fulltext(DtoSearchRequest request)
        {

            var client = ElasticClientFactory.GetElasticClient();
            var result = client.Search<DtoSearch>(s => s
            .Sort(order =>
                order.Descending(p => p.Views))
            .From(request.From)
            .Size(request.Take)
                .Query(q => q				// define query
                .MultiMatch(mp => mp			// of type MultiMatch
                    .Fields(f => f
                        .Field(p => p.Product, 3)
                        .Field(p => p.SkuName, 2.5)
                        .Field(p => p.Serving, 2.5)
                        .Field(p => p.Producer, 2)
                        .Field(p => p.Principles, 2)
                        .Field(p => p.Categories, 1))
                    .Query(request.Query)
                    .Analyzer("standard")
                    .Boost(1.1)
                    .Slop(2)
                    .Fuzziness(Fuzziness.Auto)
                    .PrefixLength(2)
                    .MaxExpansions(2)
                    .Operator(Operator.Or)
                    .MinimumShouldMatch(2)
                    .FuzzyRewrite(MultiTermQueryRewrite.ConstantScoreBoolean)
                    .TieBreaker(1.1)
                    .CutoffFrequency(0.001)
                    .Lenient()
                    .ZeroTermsQuery(ZeroTermsQuery.All)
                    )));




            var resultObj = result.Documents;
            return new DtoSearchResult()
            {
                Result = resultObj?.ToList(),
                Request = request
            };
        }
    }
}
