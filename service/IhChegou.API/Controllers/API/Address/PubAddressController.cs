using IhChegou.API.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Client;
using IhChegou.DTO.Client;
using IhChegou.WebApi.Extensions.API;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Address
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PubAddressController : IhChegouController
    {
        private IClientDomain ClientDomain;
        public PubAddressController(IClientDomain clientDomain, ICacheManager Cache) : base(Cache)
        {
            ClientDomain = clientDomain;
        }

        [HttpPost]
        [AuthorizeMobile]
        [Route("api/pub/address")]
        public DtoAddress Create(DtoAddress address)
        {
            var token = Session.ClientToken;
            ClientDomain.SaveAddress(ref address, token);
            return address;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("api/pub/address/info")]
        public DtoAddress GetInfo(DtoAddress address)
        {
            return ClientDomain.GetAddressInfo(address);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/pub/address/info")]
        public DtoAddress GetInfo2([FromUri] string postalcode = null, [FromUri]double? latitude = null, [FromUri]double? longitude = null)
        {
            //Get by postalcode
            if (postalcode != null)
                return ClientDomain.GetAddressInfo(postalcode);

            //get by latlong
            return ClientDomain.GetAddressInfo(latitude ?? 0, longitude ?? 0);
        }

        [HttpGet]
        [AuthorizeMobile]
        [Route("api/pub/address")]
        public IList<DtoAddress> Get()
        {
            var token = Session.ClientToken;
            var client = ClientDomain.Get(token);
            return client.Addresses;
        }


        [HttpDelete]
        [AuthorizeMobile]
        [Route("api/pub/address/{id}")]
        public List<DtoAddress> Delete(int id)
        {
            var token = Session.ClientToken;
            return ClientDomain.RemoveAddress(id, token);
        }
    }
}