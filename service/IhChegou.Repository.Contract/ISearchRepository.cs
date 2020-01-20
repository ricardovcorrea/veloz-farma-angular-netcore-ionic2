using System.Collections.Generic;
using IhChegou.DTO.Search;
using IhChegou.Repository.Pharma.Database;
using Nest;

namespace IhChegou.Repository.Contract
{
    public interface ISearchRepository
    {
        IElasticClient ElasticClient { get; set; }
        IProductRepository ProductQueries { get; set; }

        DtoSearchResult Fulltext(DtoSearchRequest request);
        List<string> IndexAllProducts();
        void UpdateRanking(int id, int views);
    }
}