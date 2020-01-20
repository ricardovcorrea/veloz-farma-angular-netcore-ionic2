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

namespace IhChegou.AdminStore.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]

    [RoleAuthorize(RoleType.StoreAdmin)]
    public class StoreController : IhChegouController
    {
        private IStoreDomain StoreDomain { get; set; }
        public StoreController(IStoreDomain storeDomain, ICacheManager Cache) : base(Cache)
        {
            StoreDomain = storeDomain;
        }


        [HttpPost]
        [Route("store/payment")]
        public void AddPayments([FromBody] DtoPaymentMethod paymentMethod)
        {
            var session = GetSession();
            StoreDomain.AddPaymentMethod(session.StoreId.Value, paymentMethod);
        }

        [HttpDelete]
        [Route("store/payment")]
        public void RemovePayments([FromBody] DtoPaymentMethod paymentMethod)
        {
            var session = GetSession();
            StoreDomain.RemovePaymentMethod(session.StoreId.Value, paymentMethod);
        }


        [HttpPost]
        [Route("store/delivery")]
        public void AddDelivery([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();
            StoreDomain.AddDelivery(session.StoreId.Value, delivery);
        }

        [HttpDelete]
        [Route("store/delivery")]
        public void Removedelivery([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();
            StoreDomain.RemoveDelivery(session.StoreId.Value, delivery);
        }

        [HttpDelete]
        [Route("store/officehour")]
        public void Removedelivery([FromBody] DtoDelivery delivery)
        {
            var session = GetSession();
            StoreDomain.RemoveDelivery(session.StoreId.Value, delivery);
        }

    }
}
