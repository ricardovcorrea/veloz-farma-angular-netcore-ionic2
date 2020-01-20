using IhChegou.Domain.Contract.Order;
using IhChegou.WebApi.Extensions.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IhChegou.Cache.Contract;
using IhChegou.Admin.Filters;
using IhChegou.Global.Enumerators;
using IhChegou.DTO.Order;

namespace IhChegou.Admin.Controllers
{
    public class OrderController : IhChegouController
    {
        private readonly IOrderDomain orderDomain;

        public OrderController(IOrderDomain orderDomain, ICacheManager cache) : base(cache)
        {
            this.orderDomain = orderDomain;
        }

        [RoleAuthorize(RoleType.StoreAdmin, RoleType.StoreSeller)]
        [HttpGet]
        [Route("orders")]
        public void GetOrders()
        {
            var session = GetSession();
            orderDomain.GetAvailableOrders(session.OrderId);
        }

        [RoleAuthorize(RoleType.StoreAdmin, RoleType.StoreSeller)]
        [HttpPost]
        [Route("order/{id}/response")]
        public DtoOrder AddResponse(int id, DtoOrderResponse response, DtoSkuReply sku)
        {
            var session = GetSession();
            return orderDomain.AddResponse(id, session.StoreId.Value, sku);
        }

        [RoleAuthorize(RoleType.StoreAdmin, RoleType.StoreSeller)]
        [HttpGet]
        [Route("order/{id}")]
        public DtoOrder GetUpdatedOrder(int orderId)
        {
            return orderDomain.GetRealTimeOrder(orderId);
        }
    }
}