using IhChegou.DTO.Order;
using IhChegou.Repository.Pharma.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Order
{
    public static class DeliveryParser
    {
        public static DtoDelivery ToDTO(this Delivery model)
        {
            if (model == null)
                return null;

            var dto = new DtoDelivery()
            {
                Id = model.Id,
                Price = model.Price,
                MaxDistance = model.MaxDistance
            };
            return dto;
        }
        public static List<DtoDelivery> ToDTO(this IList<Delivery> model)
        {
           return  model.Select(i => i.ToDTO()).ToList();
        }
        public static List<Delivery> ToRepository(this IList<DtoDelivery> dto)
        {
            return dto.Select(i => i.ToRepository()).ToList();
        }
        public static Delivery ToRepository(this DtoDelivery dto)
        {
            if (dto == null)
                return null;

            var model = new Delivery()
            {
                Id = dto.Id,
                Price = dto.Price,
                MaxDistance = dto.MaxDistance
            };
            return model;
        }
    }
}
