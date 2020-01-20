using IhChegou.DTO.Product;
using IhChegou.Repository.Pharma.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class PrincipleParser
    {
        public static DtoPrinciple ToDTO(this Principle model)
        {
            if (model == null)
                return null;
            var dto = new DtoPrinciple()
            {
                Id = model.Id,
                Name = model.Name
            };
            return dto;
        }

        public static List<DtoPrinciple> ToDTO(this IEnumerable<Principle> model)
        {
            if (model == null || model.Count() == 0)
                return null;
            return model.Select(i => i.ToDTO())?.ToList();
        }

        public static Principle ToRepository(this DtoPrinciple dto)
        {
            if (dto == null)
                return null;
            var model = new Principle()
            {
                Id = dto.Id,
                Name = dto.Name
            };
            return model;
        }

    }
}
