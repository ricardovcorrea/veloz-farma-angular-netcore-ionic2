using Dapper;
using IhChegou.Dto;
using IhChegou.DTO.Client;
using IhChegou.DTO.Common;
using IhChegou.DTO.Order;
using IhChegou.DTO.Store;
using IhChegou.Global.Extensions.IList;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Model.Parser.Client;
using IhChegou.Repository.Model.Parser.Order;
using IhChegou.Repository.Model.Parser.Store;
using IhChegou.Repository.Pharma.Model.Clients;
using IhChegou.Repository.Pharma.Model.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IhChegou.Repository.Pharma.Database
{
    public class StoreRepository : RepositoryBase, IStoreRepository
    {
        public StoreRepository(IDatabaseSession session) : base(session)
        {
        }

        public DtoPage<DtoStore> GetAllStores(int size, int page)
        {
            var offset = size * page;
            var stores = GetAll<Store>("Store", offset, size);
            var dtoStores = stores.Select(i => FillStore(i)).ToList();

            return new DtoPage<DtoStore>(dtoStores, size, stores.RowCount(), page);
        }
        public List<DtoStore> GetAllStores()
        {
            var stores = GetAll<Store>("Store");
            var dtoStores = stores.Select(i => FillStore(i)).ToList();

            return dtoStores;
        }


        public DtoStore Get(int id)
        {
            var store = GetById<Store>(id, "Store");
            return store.ToDTO();
        }

        public IEnumerable<DtoStore> Get(IEnumerable<int> id)
        {
            if (id == null)
            {
                return null;
            }

            var store = GetByIdIn<Store>(id.ToList(), "Store");
            return store.Select(i => FillStore(i));
        }


        public void Create(DtoStore dtoStore)
        {
            var address = dtoStore.Address.ToRepository();
            var id = Insert(address, "Address");

            var store = Store.FromDto(dtoStore);
            store.Address_Id = id;

            Insert(store, "Store");
        }

        private DtoAddress GetAddress(int id)
        {
            var address = GetById<Address>(id, "Address");
            return address.ToDTO();
        }
        private List<DtoPaymentMethod> GetPaymentMethods(int storeId)
        {
            var sql = $"SELECT " +
                      $"    PaymentMethod.*" +
                      $" FROM " +
                      $"   PaymentMethodToStore, " +
                      $"   PaymentMethod" +
                      $" WHERE" +
                      $"     PaymentMethodToStore.Store_Id = {storeId}" +
                      $"         AND PaymentMethod.Id = PaymentMethod_Id";
            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                var result = con.Query<PaymentMethod>(sql).ToList();
                return result.ToDTO();
            }
        }
        private List<DtoDelivery> GetDeliveryMethods(int storeId)
        {
            var result = GetByKeyAll<Delivery>("Store_Id", storeId, "Delivery");
            return result.ToDTO();
        }

        private IEnumerable<DtoWorkDay> GetWorkingDays(int storeId)
        {
            var result = GetByKeyAll<Workday>("Store_id", storeId, "WorkDay");
            return result.Select(i => i.ToDto());
        }

        private DtoStore FillStore(Store store)
        {
            var dtoStore = store.ToDTO();
            dtoStore.Address = GetAddress(store.Address_Id);
            dtoStore.AvailablePayments = GetPaymentMethods(store.Id);
            dtoStore.AvaibleDeliveries = GetDeliveryMethods(store.Id);
            dtoStore.WorkingDays = GetWorkingDays(store.Id);

            return dtoStore;
        }

        public void Update(DtoStore store)
        {
            var dbstore = store.ToRepository();
            this.Update(dbstore, "Store");

            if (store.Address != null)
            {
                var address = store.Address.ToRepository();

                this.Update(address, "Address");
            }
        }

        public void AddPaymentMethod(int id, DtoPaymentMethod paymentMethod)
        {
            var type = (int)paymentMethod.Type;
            var key = GetByKey<PaymentMethod>("Type", type, "PaymentMethodToStore");
            InsertFk("Store_id", id, "PaymentMethod_id", key.Id, "PaymentMethodToStore");
        }

        public void RemovePaymentMethod(int id, DtoPaymentMethod paymentMethod)
        {
            var type = (int)paymentMethod.Type;
            var dbPaymentMethod = GetByKey<PaymentMethod>("Type", type, "PaymentMethodToStore");

            var query = @"Delete  FROM PaymentMethodToStore 
                            where Store_id = @Id and PaymentMethod_id = @PaymentId";

            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    con.Execute(query, new { Id = id, PaymentId = dbPaymentMethod.Id });
                    trans.Commit();
                }
            }
        }

        public void AddDelivery(int id, DtoDelivery delivery)
        {

            var dbDelivery = delivery.ToRepository();
            dbDelivery.Store_id = id;
            Insert(dbDelivery, "Delivery");
        }

        public void RemoveDelivery(int id, DtoDelivery delivery)
        {
            var query = @"DELETE FROM Delivery 
                            WHERE
                            Id = @DeliveryId AND Store_id = @StoreId";

            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                using (var trans = con.BeginTransaction())
                {
                    con.Execute(query, new { DeliveryId = delivery.Id, StoreId = id });
                    trans.Commit();
                }
            }
        }
    }
}
