using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Query;
using IhChegou.Repository.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Respository.Factory
{
    public class FactoryRepository
    {
        private ISessionManager Session { get; set; }
        public FactoryRepository()
        {
            Session = new MysqlSession();
        }
        public static IClientQueries GetClientQuery(ISessionManager Session)
        {
            return new ClientQueries(Session);
        }
        public static IProductQueries GetProductQueries(ISessionManager Session)
        {
            return new ProductQueries(Session);
        }
        public static IOrderQueries GetIOrderQueries(ISessionManager Session)
        {
            return new OrderQueries(Session);
        }
        public static IStoreQueries GetStoreQueries(ISessionManager Session)
        {
            return new StoreQueries(Session);
        }
    }
}
