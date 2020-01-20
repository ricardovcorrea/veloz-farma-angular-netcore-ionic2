using IhChegou.Cache;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract;
using IhChegou.Domain.Contract.Client;
using IhChegou.Domain.Contract.Order;
using IhChegou.Domain.Contract.Product;
using IhChegou.Domain.Contract.Search;
using IhChegou.Domain.Contract.Store;
using IhChegou.Domain.Pharma.Client;
using IhChegou.Domain.Pharma.Order;
using IhChegou.Domain.Pharma.Product;
using IhChegou.Domain.Pharma.Search;
using IhChegou.Domain.Pharma.Store;
using IhChegou.Domain.Pharma.User;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Database;
using IhChegou.Repository.Pharma.Elastic;
using IhChegou.Repository.Session;
using IhChegou.Tools.Contract;
using IhChegou.Tools.Correios;
using IhChegou.Tools.Google.Geocode;
using SimpleInjector;

namespace IhChegou.DI
{
    public static class ConteinerExtension
    {
        public static void ApiDefaultInjection(this Container container)
        {
            //Domain
            container.Register<IClientDomain, ClientDomain>(Lifestyle.Singleton);
            container.Register<IOrderDomain, OrderDomain>(Lifestyle.Singleton);
            container.Register<IProductDomain, ProductDomain>(Lifestyle.Singleton);
            container.Register<ISearchDomain, SearchDomain>(Lifestyle.Singleton);
            container.Register<IStoreDomain, StoreDomain>(Lifestyle.Singleton);

            //Repository
            container.Register<IClientRepository, ClientRepository>(Lifestyle.Singleton);
            container.Register<IOrderRepository, OrderRepository>(Lifestyle.Singleton);
            container.Register<IProductRepository, ProductRepository>(Lifestyle.Singleton);
            container.Register<ISearchRepository, SearchRepository>(Lifestyle.Singleton);
            container.Register<IStoreRepository, StoreRepository>(Lifestyle.Singleton);

            //Session
            container.Register<IDatabaseSession, MysqlSession>(Lifestyle.Singleton);
            container.Register<IElasticSession, ElasticSession>(Lifestyle.Singleton);
            container.Register<ICacheManager, CacheManager>(Lifestyle.Singleton);
            //Tools
            container.Register<ICepApi, CepApi>(Lifestyle.Singleton);
            container.Register<IGeocodeApi, GeocodeApi>(Lifestyle.Singleton);

        }
        public static void AdmintApiInjection(this Container container)
        {
            //Domain
            container.Register<IUserDomain, UserDomain>(Lifestyle.Transient);
            container.Register<IStoreDomain, StoreDomain>(Lifestyle.Transient);

            //Repository
            container.Register<IUserRepository, UserRepository>(Lifestyle.Transient);
            container.Register<IStoreRepository, StoreRepository>(Lifestyle.Transient);

            //Session
            container.Register<IDatabaseSession, MysqlSession>(Lifestyle.Singleton);
            container.Register<IElasticSession, ElasticSession>(Lifestyle.Singleton);
            container.Register<ICacheManager, CacheManager>(Lifestyle.Singleton);
            //Tools
            container.Register<ICepApi, CepApi>(Lifestyle.Singleton);
            container.Register<IGeocodeApi, GeocodeApi>(Lifestyle.Singleton);

        }

        public static void WebToolsInjection(this Container container)
        {
            //Domain
            container.Register<IProductDomain, ProductDomain>(Lifestyle.Singleton);
            container.Register<ISearchDomain, SearchDomain>(Lifestyle.Singleton);

            //Repository
            container.Register<IProductRepository, ProductRepository>(Lifestyle.Singleton);
            container.Register<ISearchRepository, SearchRepository>(Lifestyle.Singleton);

            //Session
            container.Register<IDatabaseSession, MysqlSession>(Lifestyle.Singleton);
            container.Register<IElasticSession, ElasticSession>(Lifestyle.Singleton);

        }
    }
}
