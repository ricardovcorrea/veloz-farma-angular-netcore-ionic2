
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Store;
using IhChegou.Dto;
using IhChegou.DTO.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace IhChegou.API.Controllers.API.Store
{
    public class PubStoreController :IhChegouController
    {
        private IStoreDomain StoreDomain { get; set; }
        public PubStoreController(IStoreDomain storeDomain, ICacheManager Cache) : base(Cache)
        {
            StoreDomain = storeDomain;
        }

        [HttpGet]
        [Route("api/pub/store/near")]
        public List<DtoStore> Fulltext([FromUri]DtoGeolocation request)
        {
            return StoreDomain.GetNearStore(request);
        }
    }
}