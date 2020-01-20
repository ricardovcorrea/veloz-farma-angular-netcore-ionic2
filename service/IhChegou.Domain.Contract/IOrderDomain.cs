using System.Collections.Generic;
using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.DTO.Product;

namespace IhChegou.Domain.Contract.Order
{
    public interface IOrderDomain
    {
        DtoOrder AddResponse(int orderId, int drugStoreId, DtoSkuReply skuReplay);
        DtoOrder AddSku(int orderId, DtoSku skuRequest);
        DtoOrder CheckOrder(DtoOrder order);
        DtoOrder GenerateNewOrder(DtoClient dtoClient, int? lastAddress = default(int?));
        List<DtoOrder> GetAvailableOrders(int storeId);
        DtoOrder GetLastOrder(string clientToken);
        DtoOrder MakeOrderRequest(int orderId);
        DtoOrder SetShippping(string clientToken, DtoAddress address);
        DtoOrder SetShippping(string clientToken, int addressId);
        DtoOrder GetIncompletLastOrder(DtoClient client);
        DtoOrder GetRealTimeOrder(int orderId);
    }
}