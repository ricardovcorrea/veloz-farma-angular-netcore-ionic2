using IhChegou.DTO.Client;
using IhChegou.Repository.Model.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Client
{
    public static class AddressParser
    {
        public static DtoAddress ToDTO(this Address model)
        {
            if (model == null)
                return null;
            var dto = new DtoAddress()
            {
                Id = model.Id,
                City = model.City,
                Complement = model.Complement,
                Latitude = model.Latitude,
                Longitude = model.Longitude,
                Name = model.Name,
                Neighborhood = model.Neighborhood,
                Number = model.Number,
                PostalCode = model.PostalCode,
                Reference = model.Reference,
                State = model.State,
                Street = model.Street
            };
            return dto;
        }
        public static Address ToRepository(this DtoAddress dto, int? fk = null)
        {
            if (dto == null)
                return null;
            var model = new Address()
            {
                Id = dto.Id,
                City = dto.City,
                Complement = dto.Complement,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                Name = dto.Name,
                Neighborhood = dto.Neighborhood,
                Number = dto.Number,
                PostalCode = dto.PostalCode,
                Reference = dto.Reference,
                State = dto.State,
                Street = dto.Street,
                Client_Id = fk
            };
            return model;
        }
    }
}
