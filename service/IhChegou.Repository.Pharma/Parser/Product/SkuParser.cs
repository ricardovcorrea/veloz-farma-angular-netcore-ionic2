using IhChegou.DTO.Product;
using IhChegou.Repository.Pharma.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class SkuParser
    {
        public static List<DtoSku> ToDTO(this IList<Sku> model)
        {
            if (model == null)
                return null;

            return model.Select(i => i.ToDTO()).ToList();
        }
        public static List<DtoSku> ToDTO(this IList<SkuOrder> model)
        {
            if (model == null)
                return null;

            return model.Select(i => i.ToDTO()).ToList();
        }


        public static DtoSku ToDTO(this Sku model)
        {
            if (model == null)
                return null;
            var dto = new DtoSku()
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image
            };
            return dto;
        }
        public static DtoSku ToDTO(this SkuOrder model)
        {
            if (model == null)
                return null;
            var dto = new DtoSku()
            {
                Id = model.Id,
                Name = model.Name,
                Image = model.Image,
                Quantity = model.Quantity,
                OrderSkuId = model.Orderskurequest_Id
            };
            return dto;
        }

        public static IList<Sku> ToRepository(this IList<DtoSku> dto)
        {
            if (dto == null)
                return null;
            return dto.Select(i => i.ToRepository()).ToList();
        }
        public static Sku ToRepository(this DtoSku dto)
        {
            if (dto == null)
                return null;
            var model = new Sku()
            {
                Id = dto.Id,
                Name = dto.Name,
                Image = dto.Image,
                Product_Id = dto.ProductReference?.Id == 0 ? null : dto.ProductReference?.Id
            };
            return model;
        }
    }
}
