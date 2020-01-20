using EnsureThat;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Order;
using IhChegou.Domain.Contract.Product;
using IhChegou.Domain.Contract.Store;
using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.DTO.Product;
using IhChegou.Global.Enumerators;
using IhChegou.Global.Exceptions;
using IhChegou.Repository.Pharma.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IhChegou.Domain.Pharma.Order
{
    public class OrderDomain : IOrderDomain
    {
        private IOrderRepository OrderQueries { get; set; }
        private IClientRepository ClientQueries { get; set; }

        private IProductDomain ProductDomain { get; set; }
        private IStoreDomain StoreDomain { get; set; }
        private ICacheManager CacheManager;
        public const int ORDER_RESPONSE_EXPIRATION_TIME = 6;

        public OrderDomain(IOrderRepository orderQueries , IClientRepository clientQueries, IProductDomain productDomain, IStoreDomain storeDomain, ICacheManager cache)
        {
            OrderQueries = orderQueries;
            ClientQueries = clientQueries;

            ProductDomain = productDomain;
            StoreDomain = storeDomain;

            CacheManager = cache;
        }

        public DtoOrder GenerateNewOrder(DtoClient dtoClient, int? lastAddress = null)
        {
            try
            {
                EnsureArg.IsNotNull(dtoClient, nameof(dtoClient));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }


            var client = ClientQueries.GetClient(dtoClient.Token);
            var order = new DtoOrder()
            {
                Client = client
            };

            DtoAddress selectedAddress = null;
            if (lastAddress != null)
                selectedAddress = client.Addresses?.Where(i => i.Id == lastAddress)?.FirstOrDefault() ?? dtoClient.Addresses?.FirstOrDefault();
            else
                order.AddressToShip = client.Addresses?.FirstOrDefault();

            return CheckOrder(order);
        }

        public DtoOrder GetLastOrder(string clientToken)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(clientToken, nameof(clientToken));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var client = ClientQueries.GetClient(clientToken);
            var order = GetIncompletLastOrder(client);
            return order;
        }

        public DtoOrder SetShippping(string clientToken, DtoAddress address)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(clientToken, nameof(clientToken));
                EnsureArg.IsNotNull(address, nameof(address));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }
            ClientQueries.AddAddress(ref address, clientToken);
            return SetShippping(clientToken, address.Id);
        }

        public DtoOrder SetShippping(string clientToken, int addressId)
        {
            try
            {
                EnsureArg.IsNotNullOrEmpty(clientToken, nameof(clientToken));
                EnsureArg.IsGt(addressId, 0, nameof(addressId));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var client = ClientQueries.GetClient(clientToken);
            var order = GetIncompletLastOrder(client);

            order.AddressToShip = client.Addresses.Where(i => i.Id == addressId).SingleOrDefault();

            OrderQueries.Update(ref order);

            return CheckOrder(order);
        }

        public DtoOrder GetIncompletLastOrder(DtoClient client)
        {
            var order = OrderQueries.GetLastIncompletedOrder(client.Id);

            if (order == null)
                return GenerateNewOrder(client);

            return CheckOrder(order);
        }

        public DtoOrder AddSku(int orderId, DtoSku skuRequest)
        {
            try
            {
                EnsureArg.IsNotNull(skuRequest, nameof(skuRequest));
                EnsureArg.IsGt(orderId, 0, nameof(orderId));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var skus = OrderQueries.GetRequestedSkus(orderId);
            var dbsku = skus.Where(i => i.Id == skuRequest.Id).FirstOrDefault();
            DtoOrder dtoOrder = null;
            if (dbsku == null)
                dtoOrder = OrderQueries.AddSku(orderId, skuRequest.Id, skuRequest.Quantity.Value);
            else
            {

                dtoOrder = OrderQueries.UpdateSku(orderId, dbsku.OrderSkuId.Value, skuRequest.Quantity.Value);
            }
            return CheckOrder(dtoOrder);
        }

        public DtoOrder MakeOrderRequest(int orderId)
        {

            try
            {
                EnsureArg.IsGt(orderId, 0, nameof(orderId));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var order = OrderQueries.GetOrder(orderId);
            order.RequestedOn = DateTime.Now;
            OrderQueries.Update(ref order);

            var stores = StoreDomain.GetNearStore(order.AddressToShip);

            order.RequestedStores = stores;

            foreach (var store in stores)
            {
                OrderQueries.SetStoreOnOrder(order.Id, store.Id);
                CacheManager.SetHash(CacheKey.StoreOrders, store.Id.ToString(), order.Id.ToString(), order);
            }
            return CheckOrder(order);
        }

        public DtoOrder AddResponse(int orderId, int drugStoreId, DtoSkuReply skuReplay)
        {

            try
            {
                EnsureArg.IsGt(orderId, 0, nameof(orderId));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }


            var order = OrderQueries.GetOrder(orderId);
            var drugstore = StoreDomain.Get(drugStoreId);
            if (order.Responses != null)
            {
                order.Responses = new List<DtoOrderResponse>();
                var response = new DtoOrderResponse()
                {
                    DrugStore = drugstore,
                    Accepted = false
                };
                OrderQueries.CreateResponse(ref response, orderId);
                order.Responses.Add(response);
            }
            return CheckOrder(order);
        }

        public List<DtoOrder> GetAvailableOrders(int storeId)
        {
            try
            {
                EnsureArg.IsGt(storeId, 0, nameof(storeId));

            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }

            var response = CacheManager.GetHashs<DtoOrder>(CacheKey.StoreOrders, storeId.ToString());

            var removeItem = new List<string>();
            foreach (var item in response)
            {
                var relativeTimespace = item.Value.RequestedOn - DateTime.Now;

                if (relativeTimespace > TimeSpan.FromMinutes(ORDER_RESPONSE_EXPIRATION_TIME))
                {
                    CacheManager.DeleteHash(CacheKey.StoreOrders, storeId.ToString(), item.Key);
                    removeItem.Add(item.Key);
                }
            }
            var result = response.Where(i => !removeItem.Contains(i.Key)).Select(i => i.Value).ToList();

            var ordered = OrderQueries.GetOrderIn(result.Select(i => i.Id).ToList()).OrderBy(i => i.RequestedOn);

            return ordered.Select(i => CheckOrder(i)).ToList();
        }

        public DtoOrder GetRealTimeOrder(int orderId)
        {
            var order = OrderQueries.GetOrder(orderId);
            return CheckOrder(order);
        }

        public DtoOrder CheckOrder(DtoOrder order)
        {
            try
            {
                EnsureArg.IsNotNull(order, nameof(order));
            }
            catch (Exception ex)
            {
                throw new BadRequest(ex);
            }
            if (order.Id == 0)
                OrderQueries.Create(ref order);

            if (order.AddressToShip == null)
            {
                order.AddressToShip = order.Client?.Addresses?.FirstOrDefault();
                OrderQueries.Update(ref order);
            }

            if (order.RequestedStores == null)
                order.RequestedStores = StoreDomain.GetNearStore(order.AddressToShip);

            if (order.SkusRequesteds?.Count() > 0)
            {
                foreach (var sku in order.SkusRequesteds)
                {
                    sku.ProductReference = ProductDomain.GetProductBySku(sku.Id);
                }
            }

            return order;

            // TODO Delivery
            //if (order.AddressToShip != null && order.RequestedStores?.Count() > 0)
            //{
            //    foreach (var store in order.RequestedStores)
            //    {
            //        var clientLocation = new GeoCoordinate(order.AddressToShip.Latitude.Value, order.AddressToShip.Longitude.Value);
            //        var storeLocation = new GeoCoordinate(store.Address.Latitude.Value, store.Address.Longitude.Value);

            //        store.Proximity = clientLocation.GetDistanceTo(storeLocation);
            //        if (store.AvaibleDeliveries?.Count > 0)
            //        {
            //            var maxdistance = store.AvaibleDeliveries.Max(i => i.MaxDistance);
            //            var safeDelivery = store.AvaibleDeliveries.Where(i => i.MaxDistance == maxdistance).FirstOrDefault();

            //            var removeDeliverys = new List<DtoDelivery>();
            //            foreach (var delivery in store.AvaibleDeliveries)
            //            {
            //                if (store.Proximity > delivery.MaxDistance)
            //                    removeDeliverys.Add(delivery);
            //            }

            //            var deliveryMethod = store.AvaibleDeliveries
            //                .Where(i => !removeDeliverys.Contains(i))
            //                .OrderBy(i => i.MaxDistance).FirstOrDefault();

            //            if (deliveryMethod == null)
            //                store.Delivery = safeDelivery;
            //            else
            //                store.Delivery = deliveryMethod;
            //        }

            //    }
            //}
        }

    }
}