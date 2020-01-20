using IhChegou.DTO.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class SearchParser
    {
        public static DtoSearch ToDTO(this Repository.Model.Products.Search model)
        {
            if (model == null)
                return null;
            var dto = new DtoSearch();

            dto.Categories = model.Categories;
            dto.Principles = model.Principles;
            dto.Producer = model.Producer;
            dto.Product = model.Product;
            dto.ProductId = model.ProductId;
            dto.Serving = model.Serving;
            dto.SkuName = model.SkuName;
            dto.SkuId = model.SkuId;
            dto.Image = model.Image;

            return dto;
        }

        public static List<DtoSearch> ToDTO(this IEnumerable<Repository.Model.Products.Search> model)
        {
            if (model == null || model.Count() == 0)
                return null;
            return model.Select(i => i.ToDTO()).ToList();
        }
    }
}
