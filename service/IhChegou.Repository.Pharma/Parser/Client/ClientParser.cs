using IhChegou.DTO.Client;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Pharma.Model.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Client
{
    public static class ClientParser
    {
        public static DtoClient ToDTO(this Repository.Pharma.Model.Clients.Client model, List<Address> address = null)
        {
            if (model == null)
                return null;
            var dto = new DtoClient();
            dto.Id = model.Id;
            dto.Document = model.Document;
            dto.DeviceId = model.DeviceId;
            dto.Email = model.Email;
            dto.Name = model.Name;
            dto.Token = model.Token;
            dto.Addresses = address?.Select(i => i.ToDTO()).ToList();

            return dto;
        }
        public static Repository.Pharma.Model.Clients.Client ToRepository(this DtoClient dto)
        {
            if (dto == null)
                return null;
            var model = new Repository.Pharma.Model.Clients.Client();
            model.Id = dto.Id;
            model.Document = dto.Document;
            model.Email = dto.Email;
            model.DeviceId = dto.DeviceId;
            model.Name = dto.Name;
            model.PasswordHash = dto.PasswordHash;
            model.Token = dto.Token;
            return model;
        }
    }
}
