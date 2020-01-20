using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Search;
using IhChegou.DTO.Search;
using IhChegou.WebApi.Extensions.API;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Search
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PubSearchController : IhChegouController
    {
        private ISearchDomain SearchDomain;
        public PubSearchController(ISearchDomain searchDomain, ICacheManager Cache) : base(Cache)
        {
            SearchDomain = searchDomain;
        }

        [HttpGet]
        [Route("api/pub/fulltext")]
        public DtoSearchResult Fulltext([FromUri]DtoSearchRequest request)
        {
            return SearchDomain.Fulltext(request);
        }
    }
}