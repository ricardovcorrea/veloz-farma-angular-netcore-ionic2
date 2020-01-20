using IhChegou.API.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Client;
using IhChegou.DTO.Client;
using IhChegou.Global.Enumerators;
using IhChegou.WebApi.Extensions.API;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Client
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PubClientController : IhChegouController
    {
        private IClientDomain ClientDomain;
        public PubClientController(IClientDomain clientDomain, ICacheManager Cache) : base(Cache)
        {
            ClientDomain = clientDomain;
        }

        [HttpPost]
        [Route("api/pub/client")]
        public DtoClient CreateClient([FromBody] DtoClient client)
        {
            ClientDomain.SaveOrUpdate(ref client);
            if (client != null)
            {
                Session.ClientToken = client.Token;
                Session.OrderId = client.AtualOrder.Id;
                Session.MobileAuthenticate = true;
                return client;
            }
            return client;
        }

        [HttpGet]
        [Route("api/pub/client/login")]
        public DtoClient Login([FromUri]string email, [FromUri]string password)
        {
            var client = ClientDomain.Login(email, password);
            if (client != null)
            {
                Session.ClientToken = client.Token;
                Session.OrderId = client.AtualOrder.Id;
                Session.MobileAuthenticate = true;
                return client;
            }
            return client;
        }

        [HttpGet]
        [Route("api/pub/client/logout")]
        public void Logout()
        {
            CacheManager.Delete(CacheKey.Session, Session.SessionId);
        }

        [HttpGet]
        [AuthorizeMobile]
        [Route("api/pub/client")]
        public DtoClient Get()
        {
            var token = Session.ClientToken;
            var client = ClientDomain.Get(token);
            Session.OrderId = client.AtualOrder.Id;
            return client;
        }

        [HttpPut]
        [AuthorizeMobile]
        [Route("api/pub/client")]
        public DtoClient Update([FromBody] DtoClient client)
        {
            client.Token = Session.ClientToken;
            ClientDomain.SaveOrUpdate(ref client);
            return client;
        }
    }
}