using Dapper;
using IhChegou.Dto;
using IhChegou.DTO.Order;
using IhChegou.DTO.Product;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Model.Parser.Order;
using IhChegou.Repository.Model.Parser.Product;
using IhChegou.Repository.Model.Parser.Store;
using IhChegou.Repository.Pharma.Model.Orders;
using IhChegou.Repository.Pharma.Model.Products;
using IhChegou.Repository.Pharma.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Pharma.Database
{
    public class OrderRepository : RepositoryBase, IOrderRepository
    {
        private IClientRepository ClientQueries { get; set; }
        private IStoreRepository StoreQueries { get; set; }
        private IProductRepository ProductQueries { get; set; }


        public OrderRepository(IDatabaseSession session):base(session)
        {
            ClientQueries = new ClientRepository(session);
            StoreQueries = new StoreRepository(session);
            ProductQueries = new ProductRepository(session);

        }
        public void Create(ref DtoOrder order)
        {
            var model = order.ToRepository();
            var insertedId = Insert(model, "Orderform");
            order.Id = insertedId;
        }
        public void CreateResponse(ref DtoOrderResponse response, int orderId)
        {
            var model = response.ToRepository(orderId);
            var insertedId = Insert(model, "OrderResponse");
            response.Id = insertedId;
        }

        public void Update(ref DtoOrder order)
        {
            var model = order.ToRepository();
            Update(model, "Orderform");
        }

        public void SetStoreOnOrder(int order_id, int store_id)
        {
            var storeOrder = new StoreToOrder()
            {
                Order_id = order_id,
                Store_id = store_id
            };
            Insert(storeOrder, "StoreToOrder");
        }



        public DtoOrder AddSku(int orderId, int id, int quantity)
        {
            var sku = new OrderSkuRequest()
            {
                Order_Id = orderId,
                Quantity = quantity,
                Sku_Id = id
            };
            Insert(sku, "OrderSkuRequest");
            return FillOrder(orderId);
        }
        public DtoOrder UpdateSku(int orderId, int orderSkuId, int quantity)
        {
            var dbrequest = GetById<OrderSkuRequest>(orderSkuId, "OrderSkuRequest");
            dbrequest.Quantity = quantity;
            Update(dbrequest, "OrderSkuRequest");
            return FillOrder(orderId);
        }



        public DtoOrder GetLastIncompletedOrder(int clientId)
        {
            var sql = $"SELECT * FROM Orderform " +
                $" WHERE Client_Id = {clientId}" +
                $" AND DeliveredOn is null" +
                $" Order by Id desc;";

            Order dbOrder = null;
            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                dbOrder = con.Query<Order>(sql).SingleOrDefault();
            }
            return FillOrder(dbOrder);
        }

        public DtoOrder GetOrder(int orderId)
        {
            var dborder = GetById<Order>(orderId, "Orderform");
            return FillOrder(dborder);
        }

        public List<DtoOrder> GetOrderIn(List<int> list)
        {
            var result = GetByIdIn<Order>(list, "Orderform");
            return result?.Select(i => FillOrder(i))?.ToList();
        }

        public List<DtoSku> GetRequestedSkus(int orderId)
        {
            var sql = $"SELECT Sku.*," +
                $" OrderSkuRequest.Quantity," +
                $" OrderSkuRequest.Id OrderSkuRequest_Id" +
                $" FROM Sku, OrderSkuRequest" +
                $" WHERE OrderSkuRequest.Order_Id = {orderId}" +
                $" AND Sku.Id = OrderSkuRequest.Sku_Id";

            var skus = new List<SkuOrder>();
            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                skus = con.Query<SkuOrder>(sql).AsList();
            }

            return skus.ToDTO();
        }
        public List<DtoStore> GetRequestedStores(int id)
        {
            var sql = $"SELECT " +
                $"    Store.*" +
                $" FROM" +
                $"    StoreToOrder," +
                $"    Store" +
                $" WHERE" +
                $"    Order_id = {id} AND Store.Id = Store_id";

            List<Store> stores = null;
            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                stores = con.Query<Store>(sql).ToList();
            }
            return stores?.Select(i => i.ToDTO()).ToList();
        }
        public List<DtoOrderResponse> GetOrderResponses(int id)
        {
            var result = GetByKeyAll<OrderResponse>("Order_id", id, "OrderResponse");
            return result?.Select(i => FillOrderResponse(i))?.ToList();
        }

        public List<DtoSkuReply> GetSkuReplay(int id)
        {
            var result = GetByKeyAll<SkuReply>("OrderResponse_id", id, "SkuReplay");
            return result?.Select(i => FillSkuReplay(i))?.ToList();
        }


        private DtoOrder FillOrder(int orderId)
        {
            var dborder = GetById<Order>(orderId, "Orderform");
            return FillOrder(dborder);

        }
        private DtoOrder FillOrder(Order dbOrder)
        {
            if (dbOrder == null)
                return null;
            var order = dbOrder.ToDTO();

            if (dbOrder.Client_Id != null)
                order.Client = ClientQueries.GetClient(dbOrder.Client_Id.Value);

            if (dbOrder.AddressToShip_Id.HasValue)
                order.AddressToShip = ClientQueries.GetAddress(dbOrder.AddressToShip_Id.Value);

            order.SkusRequesteds = GetRequestedSkus(dbOrder.Id);

            order.RequestedStores = GetRequestedStores(dbOrder.Id);

            order.Responses = GetOrderResponses(dbOrder.Id);
            return order;
        }
        private DtoOrderResponse FillOrderResponse(OrderResponse dbResponse)
        {
            if (dbResponse == null)
                return null;
            var dtoResponse = dbResponse.ToDTO();

            dtoResponse.DrugStore = StoreQueries.Get(dbResponse.DrugStore_id);
            dtoResponse.SkuReplys = GetSkuReplay(dtoResponse.Id);
            return dtoResponse;
        }
        private DtoSkuReply FillSkuReplay(SkuReply skureplay)
        {
            var dto = skureplay.ToDTO();

            if (skureplay.OferredSku_id.HasValue)
                dto.OferredSku = ProductQueries.GetSku(skureplay.OferredSku_id.Value);
            if (skureplay.OriginalSku_id.HasValue)
                dto.OriginalSku = ProductQueries.GetSku(skureplay.OriginalSku_id.Value);
            return dto;
        }



    }
}
