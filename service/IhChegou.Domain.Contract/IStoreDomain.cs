using System.Collections.Generic;
using IhChegou.Dto;
using IhChegou.DTO.Client;
using IhChegou.DTO.Common;
using IhChegou.DTO.Order;

namespace IhChegou.Domain.Contract.Store
{
    public interface IStoreDomain
    {
        void Create(DtoStore store);
        DtoStore Get(int id);
        void Update(DtoStore store);
        List<DtoStore> GetAll();
        DtoPage<DtoStore> GetAll(int size, int page);
        List<DtoStore> GetNearStore(DtoAddress address);
        List<DtoStore> GetNearStore(DtoGeolocation request);
        void AddPaymentMethod(int id,DtoPaymentMethod paymentMethod);
        void RemovePaymentMethod(int id, DtoPaymentMethod paymentMethod);
        void AddDelivery(int id, DtoDelivery delivery);
        void RemoveDelivery(int id, DtoDelivery delivery);
    }
}