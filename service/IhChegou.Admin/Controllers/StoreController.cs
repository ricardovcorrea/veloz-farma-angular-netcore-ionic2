using IhChegou.Admin.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Store;
using IhChegou.Dto;
using IhChegou.DTO.Client;
using IhChegou.DTO.Common;
using IhChegou.DTO.Order;
using IhChegou.Global.Enumerators;
using IhChegou.WebApi.Extensions.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.Admin.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class StoreController : IhChegouController
    {
        private IStoreDomain StoreDomain { get; set; }
        public StoreController(IStoreDomain storeDomain, ICacheManager Cache) : base(Cache)
        {
            StoreDomain = storeDomain;
        }

        [HttpPost]
        [RoleAuthorize(RoleType.God)]

        [Route("store")]
        public void CreateStore(DtoStore store)
        {
            StoreDomain.Create(store);
        }

        [HttpGet]
        [RoleAuthorize(RoleType.God)]
        [Route("store")]
        public DtoPage<DtoStore> GetStores(int size = 10, int page = 0)
        {
            return StoreDomain.GetAll(size, page);
        }

        [HttpPut]
        [RoleAuthorize(RoleType.God)]
        [Route("store")]
        [RoleAuthorize(RoleType.God)]
        public void GetStores(DtoStore store)
        {
            StoreDomain.Update(store);
        }

        [HttpPost]
        [RoleAuthorize(RoleType.God)]
        [Route("store/{id}/payment")]
        public void AddPayments(int id, [FromBody] DtoPaymentMethod paymentMethod)
        {
            StoreDomain.AddPaymentMethod(id, paymentMethod);
        }

        [HttpDelete]
        [RoleAuthorize(RoleType.God)]
        [Route("store/{id}/payment")]
        public void RemovePayments(int id, [FromBody] DtoPaymentMethod paymentMethod)
        {
            StoreDomain.RemovePaymentMethod(id, paymentMethod);
        }


        [HttpPost]
        [RoleAuthorize(RoleType.God)]
        [Route("store/{id}/delivery")]
        public void AddDelivery(int id, [FromBody] DtoDelivery delivery)
        {
            StoreDomain.AddDelivery(id, delivery);
        }

        [HttpDelete]
        [RoleAuthorize(RoleType.God)]
        [Route("store/{id}/delivery")]
        public void Removedelivery(int id, [FromBody] DtoDelivery delivery)
        {
            StoreDomain.RemoveDelivery(id, delivery);
        }

        [HttpPost]
        [RoleAuthorize(RoleType.StoreAdmin)]
        [Route("store/payment")]
        public void AddPayments([FromBody] DtoPaymentMethod paymentMethod)
        {
            var session = GetSession();
            StoreDomain.AddPaymentMethod(session.StoreId.Value, paymentMethod);
        }

        [HttpDelete]
        [RoleAuthorize(RoleType.StoreAdmin)]
        [Route("store/payment")]
        public void RemovePayments([FromBody] DtoPaymentMethod paymentMethod)
        {
            var session = GetSession();
            StoreDomain.RemovePaymentMethod(session.StoreId.Value, paymentMethod);
        }


        [HttpPost]
        [RoleAuthorize(RoleType.StoreAdmin)]
        [Route("store/delivery")]
        public void AddDelivery([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();
            StoreDomain.AddDelivery(session.StoreId.Value, delivery);
        }

        [HttpDelete]
        [RoleAuthorize(RoleType.StoreAdmin)]
        [Route("store/delivery")]
        public void Removedelivery([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();
            StoreDomain.RemoveDelivery(session.StoreId.Value, delivery);
        }

        [HttpDelete]
        [RoleAuthorize(RoleType.StoreAdmin)]
        [Route("store/officehour")]
        public void mimi([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();

            StoreDomain.RemoveDelivery(session.StoreId.Value, delivery);
        }

    }
}
