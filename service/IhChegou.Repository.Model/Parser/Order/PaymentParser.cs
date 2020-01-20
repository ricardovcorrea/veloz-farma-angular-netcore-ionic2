using IhChegou.DTO.Order;
using IhChegou.Repository.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Order
{
    public static class PaymentParser
    {
        public static DtoPaymentMethod ToDTO(this PaymentMethod model)
        {
            if (model == null)
                return null;

            var dto = new DtoPaymentMethod()
            {
                Id = model.Id,
                Type = model.Type
            };
            return dto;
        }
        public static PaymentMethod ToRepository(this DtoPaymentMethod dto)
        {
            if (dto == null)
                return null;

            var model = new PaymentMethod()
            {
                Id = dto.Id,
                Type = dto.Type
            };
            return model;
        }

        public static List<DtoPaymentMethod> ToDTO(this List<PaymentMethod> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }
        public static List<PaymentMethod> ToRepository(this IList<DtoPaymentMethod> dto)
        {
            if (dto == null)
                return null;

            return dto.Select(i => i.ToRepository()).ToList();
        }
    }
}
