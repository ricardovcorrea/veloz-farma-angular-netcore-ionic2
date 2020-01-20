using IhChegou.DTO.Product;
using IhChegou.Repository.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Product
{
    public static class ProducerParser
    {
        public static DtoProducer ToDTO(this Producer model)
        {
            if (model == null)
                return null;
            var dto = new DtoProducer();
            dto.Id = model.Id;
            dto.Name = model.Name;
            return dto;
        }
        public static List<DtoProducer> ToDTO(this IEnumerable<Producer> model)
        {
            if (model == null || model.Count() == 0)
                return null;
            return model.Select(i => i.ToDTO())?.ToList();
        }
        public static Producer ToRepository(this DtoProducer dto)
        {
            if (dto == null)
                return null;
            var model = new Producer();
            model.Id = dto.Id;
            model.Name = dto.Name;
            return model;
        }

    }
}
