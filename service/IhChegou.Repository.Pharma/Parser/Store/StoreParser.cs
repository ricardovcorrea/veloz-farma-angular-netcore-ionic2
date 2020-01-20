using IhChegou.Dto;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Model.Parser.Order;
using IhChegou.Repository.Pharma.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Store
{
    public static class StoreParser
    {
        public static DtoStore ToDTO(this Pharma.Model.Stores.Store model)
        {
            if (model == null)
                return null;

            var dto = new DtoStore()
            {
                Id = model.Id,
                Document = model.Document,
                Name = model.Name,
                MaxDistance = model.MaxDistance,
                Profit = model.Profit
            };
            return dto;
        }
        public static List<DtoStore> ToDTO(this IList<IhChegou.Repository.Pharma.Model.Stores.Store> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }
        public static IhChegou.Repository.Pharma.Model.Stores.Store ToRepository(this DtoStore dto)
        {
            if (dto == null)
                return null;

            var model = new IhChegou.Repository.Pharma.Model.Stores.Store()
            {
                Id = dto.Id,
                MaxDistance = dto.MaxDistance,
                Document = dto.Document,
                Name = dto.Name,
                Profit = dto.Profit
            };
            return model;
        }
        public static List<IhChegou.Repository.Pharma.Model.Stores.Store> ToRepository(this List<DtoStore> dto)
        {
            return dto?.Select(i => i?.ToRepository())?.ToList();
        }
    }
}
