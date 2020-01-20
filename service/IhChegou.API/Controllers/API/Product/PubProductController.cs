using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract.Product;
using IhChegou.DTO.Product;
using IhChegou.WebApi.Extensions.API;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.API.Controllers.API.Product
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PubProductController : IhChegouController
    {
        private IProductDomain ProductDomain;
        public PubProductController(IProductDomain productDomain, ICacheManager Cache) : base(Cache)
        {
            ProductDomain = productDomain;
        }
        [HttpGet]
        [Route("api/pub/product/{id}")]
        public DtoProduct GetProduct(int id)
        {
            return ProductDomain.GetProduct(id);
        }

        [HttpGet]
        [Route("api/pub/product/sku/{id}")]
        public DtoSku GetSku(int id)
        {
            return ProductDomain.GetSku(id);
        }
    }
}