using IhChegou.DTO.Order;
using IhChegou.Repository.Pharma.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Order
{
    public static class OrderResponseParser
    {
        public static OrderResponse ToRepository(this DtoOrderResponse dto, int order_id)
        {
            if (dto == null)
                return null;

            var model = new OrderResponse()
            {
                Id = dto.Id,
                Accepted = dto.Accepted,
                Order_id = order_id,
                DrugStore_id = dto.DrugStore.Id
            };
            return model;
        }
        public static DtoOrderResponse ToDTO(this OrderResponse model)
        {
            if (model == null)
                return null;

            var dto = new DtoOrderResponse()
            {
                Id = model.Id,
                Accepted = model.Accepted,
                
            };
            return dto;
        }

        public static List<DtoOrderResponse> ToDTO(this IList<OrderResponse> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }
        public static IList<OrderResponse> ToRepository(this IList<DtoOrderResponse> dto, int orderId)
        {
            if (dto == null)
                return null;

            return dto.Select(i => i.ToRepository(orderId)).ToList();
        }

    }
}
