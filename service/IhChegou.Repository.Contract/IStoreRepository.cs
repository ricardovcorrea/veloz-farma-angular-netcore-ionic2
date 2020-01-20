using System.Collections.Generic;
using IhChegou.Dto;
using IhChegou.DTO.Common;
using IhChegou.DTO.Order;

namespace IhChegou.Repository.Pharma.Database
{
    public interface IStoreRepository
    {
        void Create(DtoStore store);
        void Update(DtoStore store);
        DtoStore Get(int id);
        IEnumerable<DtoStore> Get(IEnumerable<int> id);
        DtoPage<DtoStore> GetAllStores(int size, int page);
        List<DtoStore> GetAllStores();
        void AddPaymentMethod(int id, DtoPaymentMethod paymentMethod);
        void RemovePaymentMethod(int id, DtoPaymentMethod paymentMethod);
        void AddDelivery(int id, DtoDelivery delivery);
        void RemoveDelivery(int id, DtoDelivery delivery);
    }
}
