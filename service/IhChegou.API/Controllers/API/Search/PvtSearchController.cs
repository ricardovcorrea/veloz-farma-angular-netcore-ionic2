using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Search;
using IhChegou.WebApi.Extensions.API;
using System.Collections.Generic;
using System.Web.Http;

namespace IhChegou.API.Controllers.API.Search
{
    public class PvtSearchController : IhChegouController
    {
        private ISearchDomain SearchDomain;
        public PvtSearchController(ISearchDomain searchDomain, ICacheManager Cache) : base(Cache)
        {
            SearchDomain = searchDomain;
        }

        [HttpPatch]
        [Route("api/ElasticSearch")]
        public List<string> ElasticUpdate()
        {
            return SearchDomain.SetProductsOnElastic();
        }
    }
}