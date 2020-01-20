using System.Collections.Generic;
using System.Linq;
using IhChegou.Dto;
using IhChegou.DTO.Common;
using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.Global.Extensions.String;
using IhChegou.Global.Exceptions;
using IhChegou.Repository.Pharma.Database;
using IhChegou.Domain.Contract.Store;
using System.Device.Location;

namespace IhChegou.Domain.Pharma.Store
{
    public class StoreDomain : IStoreDomain
    {
        private IStoreRepository storeRepository { get; set; }
        public StoreDomain(IStoreRepository storeQueries)
        {
            storeRepository = storeQueries;
        }

        public DtoPage<DtoStore> GetAll(int size, int page)
        {
            var result = storeRepository.GetAllStores(size, page);
            return result;
        }
        public List<DtoStore> GetAll()
        {
            var result = storeRepository.GetAllStores();
            return result;
        }
        public DtoStore Get(int id)
        {
            var result = storeRepository.Get(id);
            return result;
        }

        public List<DtoStore> GetNearStore(DtoAddress address)
        {
            if (address == null || (address.Latitude == null || address.Longitude == null))
                return null;
            return GetNearStore(new DtoGeolocation()
            {
                Latitude = address.Latitude.Value,
                Longitude = address.Longitude.Value
            });
        }


        public List<DtoStore> GetNearStore(DtoGeolocation request)
        {
            var mylocation = new GeoCoordinate(request.Latitude, request.Longitude);

            var stores = storeRepository.GetAllStores();

            foreach (var store in stores)
            {
                var storeLocation = new GeoCoordinate(store.Address.Latitude.Value, store.Address.Longitude.Value);
                store.Proximity = mylocation.GetDistanceTo(storeLocation);

                // removendo deliverys não alcançados
                store.AvaibleDeliveries = store.AvaibleDeliveries
                    .Where(i => i.MaxDistance > store.Proximity).ToList();
            }

            // removendo stores sem deliverys
            stores = stores.Where(i => i.AvaibleDeliveries.Count() > 0).ToList();


            return stores.Where(i => i.Proximity <= i.MaxDistance).ToList();
        }

        public void Create(DtoStore store)
        {
            storeRepository.Create(store);
        }

        public void Update(DtoStore store)
        {
            storeRepository.Update(store);
        }

        public void AddPaymentMethod(int id, DtoPaymentMethod paymentMethod)
        {
            storeRepository.AddPaymentMethod(id, paymentMethod);
        }


        public void RemovePaymentMethod(int id, DtoPaymentMethod paymentMethod)
        {
            storeRepository.RemovePaymentMethod(id, paymentMethod);
        }

        public void AddDelivery(int id, DtoDelivery delivery)
        {
            storeRepository.AddDelivery(id, delivery);
        }

        public void RemoveDelivery(int id, DtoDelivery delivery)
        {
            storeRepository.RemoveDelivery(id, delivery);
        }
    }
}
