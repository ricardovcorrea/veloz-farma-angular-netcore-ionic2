using IhChegou.DTO.Order;
using IhChegou.Repository.Model.Parser.Product;
using IhChegou.Repository.Pharma.Model.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Order
{
    public static class SkuReplyParser
    {
        public static DtoSkuReply ToDTO(this SkuReply model)
        {
            if (model == null)
                return null;

            var dto = new DtoSkuReply()
            {
                Id = model.Id,
                OutStock = model.OutStock,
                Price = model.Price
            };
            return dto;
        }
        public static IList<DtoSkuReply> ToDTO(this IList<SkuReply> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }

        public static IList<SkuReply> ToRepository(this IList<DtoSkuReply> dto)
        {
            if (dto == null)
                return null;

            return dto.Select(i => i.ToRepository()).ToList();
        }
        public static SkuReply ToRepository(this DtoSkuReply dto)
        {
            if (dto == null)
                return null;

            var model = new SkuReply()
            {
                Id = dto.Id,
                //OferredSku = dto.OferredSku.ToRepository(),
                //OriginalSku = dto.OriginalSku.ToRepository(),
                OutStock = dto.OutStock,
                Price = dto.Price
            };
            return model;
        }
    }
}
