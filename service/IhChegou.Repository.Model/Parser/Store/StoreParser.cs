using IhChegou.Dto;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Model.Parser.Order;
using IhChegou.Repository.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Store
{
    public static class StoreParser
    {
        public static DtoStore ToDTO(this Stores.Store model)
        {
            if (model == null)
                return null;

            var dto = new DtoStore()
            {
                Id = model.Id,
                Document = model.Document,
                Email = model.Email,
                Name = model.Name,
                MaxDistance = model.MaxDistance,
                PasswordHash = model.PasswordHash,
                Profit = model.Profit
            };
            return dto;
        }
        public static List<DtoStore> ToDTO(this IList<Stores.Store> model)
        {
            return model.Select(i => i.ToDTO()).ToList();
        }
        public static Stores.Store ToRepository(this DtoStore dto)
        {
            if (dto == null)
                return null;

            var model = new Stores.Store()
            {
                Id = dto.Id,
                MaxDistance = dto.MaxDistance,
                Document = dto.Document,
                Email = dto.Email,
                Name = dto.Name,
                PasswordHash = dto.PasswordHash,
                Profit = dto.Profit
            };
            return model;
        }
        public static List<Stores.Store> ToRepository(this List<DtoStore> dto)
        {
            return dto?.Select(i => i?.ToRepository())?.ToList();
        }
    }
}
