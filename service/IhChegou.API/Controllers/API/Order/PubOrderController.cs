using IhChegou.API.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Order;
using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.DTO.Product;
using IhChegou.WebApi.Extensions.API;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Order
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PubOrderController : IhChegouController
    {
        private IOrderDomain OrderDomain;
        public PubOrderController(IOrderDomain orderDomain, ICacheManager Cache) : base(Cache)
        {
            OrderDomain = orderDomain;
        }

        [HttpGet]
        [AuthorizeMobile]
        [Route("api/pub/order")]
        public DtoOrder GetOrder()
        {
            var clientToken = Session.ClientToken;
            var result = OrderDomain.GetLastOrder(clientToken);
            return result;
        }

        [HttpPost]
        [AuthorizeMobile]
        [Route("api/pub/order/sku")]
        public DtoOrder AddSku([FromBody]DtoSku sku)
        {
            var orderId = Session.OrderId;
            var result = OrderDomain.AddSku(orderId, sku);
            return result;
        }
        [HttpPost]
        [AuthorizeMobile]
        [Route("api/pub/order/address/{id}")]
        public DtoOrder SetShipping(int id)
        {
            var clientToken = Session.ClientToken;
            var result = OrderDomain.SetShippping(clientToken, id);
            Session.LastSelectedAddress = id;
            return result;
        }

        [HttpPost]
        [AuthorizeMobile]
        [Route("api/pub/order/address")]
        public DtoOrder SetNewShipping([FromBody] DtoAddress address)
        {
            var clientToken = Session.ClientToken;
            var result = OrderDomain.SetShippping(clientToken, address);
            Session.LastSelectedAddress = address.Id;
            return result;
        }

        [HttpPost]
        [AuthorizeMobile]
        [Route("api/pub/order/request")]
        public DtoOrder MakeOrderRequest()
        {
            var orderId = Session.OrderId;
            var result = OrderDomain.MakeOrderRequest(orderId);
            return result;
        }
    }
}