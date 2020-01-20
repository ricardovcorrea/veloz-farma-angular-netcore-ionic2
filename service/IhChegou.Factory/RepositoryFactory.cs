using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Database;
using IhChegou.Repository.Pharma.Elastic;
using IhChegou.Repository.Session;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Factory
{
    public class RepositoryFactory
    {
        private IDatabaseSession Session { get; set; }
        private IElasticClient ElasticClient { get; set; }
        public RepositoryFactory()
        {
            Session = new MysqlSession();
            ElasticClient = new ElasticSession().GetElasticClient();
        }
        public  IClientRepository GetClientRepository()
        {
            return new ClientRepository(Session);
        }
        public  IProductRepository GetProductRepository()
        {
            return new ProductRepository(Session);
        }
        public  IOrderRepository GetIOrderRepository()
        {
            return new OrderRepository(Session);
        }
        public  IStoreRepository GetStoreRepository()
        {
            return new StoreRepository(Session);
        }
        public ISearchRepository GetSearchRepository()
        {
            return new SearchRepository(GetProductRepository(), ElasticClient);
        }
    }
}
