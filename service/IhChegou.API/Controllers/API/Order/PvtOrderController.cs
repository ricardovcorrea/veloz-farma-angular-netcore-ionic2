using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Order;
using IhChegou.DTO.Order;
using IhChegou.WebApi.Extensions.API;
using System.Collections.Generic;

namespace IhChegou.API.Controllers.API.Order
{
    public class PvtOrderController : IhChegouController
    {
        private IOrderDomain OrderDomain;
        public PvtOrderController(IOrderDomain orderDomain, ICacheManager Cache) : base(Cache)
        {
            OrderDomain = orderDomain;
        }
        public List<DtoOrder> GetAvailableOrders(int storeId)
        {
            return OrderDomain.GetAvailableOrders(storeId);
        }
    }
}
