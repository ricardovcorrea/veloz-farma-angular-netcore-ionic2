using System.Collections.Generic;
using IhChegou.Dto;
using IhChegou.DTO.Order;
using IhChegou.DTO.Product;

namespace IhChegou.Repository.Pharma.Database
{
    public interface IOrderRepository
    {
        DtoOrder AddSku(int orderId, int id, int quantity);
        void Create(ref DtoOrder order);
        void CreateResponse(ref DtoOrderResponse response, int orderId);
        DtoOrder GetLastIncompletedOrder(int clientId);
        DtoOrder GetOrder(int orderId);
        List<DtoOrder> GetOrderIn(List<int> list);
        List<DtoOrderResponse> GetOrderResponses(int id);
        List<DtoSku> GetRequestedSkus(int orderId);
        List<DtoStore> GetRequestedStores(int id);
        List<DtoSkuReply> GetSkuReplay(int id);
        void SetStoreOnOrder(int order_id, int store_id);
        void Update(ref DtoOrder order);
        DtoOrder UpdateSku(int orderId, int orderSkuId, int quantity);
    }
}