using IhChegou.DTO.Product;
using IhChegou.Repository.Pharma.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class CategoryParser
    {
        public static DtoCategory ToDTO(this Category model)
        {
            if (model == null)
                return null;
            var dto = new DtoCategory();
            dto.Id = model.Id;
            dto.Name = model.Name;

            return dto;
        }

        public static List<DtoCategory> ToDTO(this IEnumerable<Category> model)
        {
            if (model == null || model.Count() == 0)
                return null;

            return model.Select(i => i.ToDTO())?.ToList();
        }
        public static Category ToRepository(this DtoCategory dto)
        {
            if (dto == null)
                return null;
            var model = new Category();
            model.Id = dto.Id;
            model.Name = dto.Name;
            model.Category_id = dto.RefCategory?.Id;

            return model;
        }
        public static List<Category> ToRepository(this IEnumerable<DtoCategory> dto)
        {
            if (dto == null || dto.Count() == 0)
                return null;
            return dto.Select(i => i.ToRepository())?.ToList();
        }
    }
}
