using IhChegou.API.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Store;
using IhChegou.Dto;
using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Store
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PvtStoreController : IhChegouController
    {
        private IStoreDomain StoreDomain { get; set; }
        public PvtStoreController(IStoreDomain storeDomain, ICacheManager Cache) : base(Cache)
        {
            StoreDomain = storeDomain;
        }



        [HttpPost]
        [Route("api/pvt/store")]
        public void CreateStore(DtoStore store)
        {
            StoreDomain.Create(store);
        }

        [HttpGet]
        [AuthorizeAdmin]
        [Route("api/pvt/store")]
        public DtoStore GetStores()
        {
            return StoreDomain.Get(Session.StoreId.Value);
        }

        //[HttpGet]
        //[AuthorizeAdmin]
        //[Route("api/pvt/store/activeOrders")]
        //public List<DtoOrder> GetActiveOrders()
        //{
        //    return StoreDomain.GetAvailableOrders(Session.StoreId.Value);
        //}

        [HttpPost]
        [Route("api/pvt/store/{id}/address")]
        public DtoAddress AddAdress(int id, [FromBody] DtoAddress address)
        {
            return StoreDomain.AddAdress(id, address);
        }

        [HttpPost]
        [Route("api/pvt/store/{id}/paymentmethods")]
        public List<DtoPaymentMethod> SetPaymentMethods(int id, [FromBody] List<DtoPaymentMethod> methods)
        {
            return StoreDomain.SetPaymentMethods(id, methods);
        }
        [HttpPost]
        [Route("api/pvt/store/{id}/deliveryethods")]
        public List<DtoDelivery> SetDeliveryMethods(int id, [FromBody] List<DtoDelivery> methods)
        {
            return StoreDomain.SetDeliveryMethods(id, methods);
        }

    }
}
