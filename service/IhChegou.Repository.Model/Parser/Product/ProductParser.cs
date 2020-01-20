using IhChegou.DTO.Product;
using IhChegou.Repository.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class ProductParser
    {
        public static List<DtoProduct> ToDto(this IList<Products.Product> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }
        public static DtoProduct ToDTO(this Repository.Model.Products.Product model)
        {
            if (model == null)
                return null;
            var dto = new DtoProduct()
            {
                Id = model.Id,
                Name = model.Name,
                NeedRecipe = model.NeedRecipe,
                Serving = model.Serving,
                Type = model.Type,
                Views = model.Views
            };
            return dto;
        }
        public static Products.Product ToRepository(this DtoProduct dto)
        {
            if (dto == null)
                return null;
            var model = new Products.Product()
            {
                Id = dto.Id,
                Name = dto.Name,
                NeedRecipe = dto.NeedRecipe,
                Serving = dto.Serving,
                Views = dto.Views,
                Type = dto.Type,
                Producer_Id = dto.Producer?.Id
            };
            return model;
        }
    }
}
