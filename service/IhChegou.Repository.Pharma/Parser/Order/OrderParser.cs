using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Model.Parser.Product;
using IhChegou.Repository.Model.Parser.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Parser.Order
{
    public static class OrderParser
    {
        public static Repository.Pharma.Model.Orders.Order ToRepository(this DtoOrder dto)
        {
            if (dto == null)
                return null;

            var model = new Repository.Pharma.Model.Orders.Order()
            {
                Id = dto.Id,
                AgreesReference = dto.AgreesReference,
                AgreesSimilar = dto.AgreesSimilar,
                AgressGeneric = dto.AgressGeneric,
                DeliveredOn = dto.DeliveredOn,
                RequestedOn = dto.RequestedOn,
                ScheduledTo = dto.ScheduledTo,
                Client_Id = dto.Client?.Id,
                AddressToShip_Id = dto.AddressToShip?.Id
            };
            return model;

        }
        public static DtoOrder ToDTO(this Repository.Pharma.Model.Orders.Order model)
        {
            if (model == null)
                return null;

            var dto = new DtoOrder()
            {
                Id = model.Id,
                AgreesReference = model.AgreesReference,
                AgreesSimilar = model.AgreesSimilar,
                AgressGeneric = model.AgressGeneric,
                DeliveredOn = model.DeliveredOn,
                RequestedOn = model.RequestedOn,
                ScheduledTo = model.ScheduledTo,
                
            };
            return dto;

        }
        public static List<DtoOrder> ToDTO(this IList<Repository.Pharma.Model.Orders.Order> model)
        {
           return  model.Select(i => i.ToDTO()).ToList();
        }


    }
}
