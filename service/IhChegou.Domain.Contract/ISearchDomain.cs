using System.Collections.Generic;
using IhChegou.DTO.Search;

namespace IhChegou.Domain.Contract.Search
{
    public interface ISearchDomain
    {

        DtoSearchResult Fulltext(DtoSearchRequest request);
        DtoCategoryTree GetCategoryTree();
        List<string> SetProductsOnElastic();
    }
}