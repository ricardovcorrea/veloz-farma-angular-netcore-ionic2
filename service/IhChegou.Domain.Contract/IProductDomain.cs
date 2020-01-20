using System.Collections.Generic;
using IhChegou.DTO.Product;

namespace IhChegou.Domain.Contract.Product
{
    public interface IProductDomain
    {
        List<DtoSku> GetAllSkus(int page = 0, int size = 50);
        List<DtoSku> GetAllSkusWithoutImage(int page = 0, int size = 50);
        DtoProduct GetProduct(int id);
        DtoProduct GetProductBySku(int id);
        List<DtoProduct> GetProductLike(string name, int page, int size);
        List<DtoProduct> GetProductOrderByRank(int page, int size);
        DtoSku GetSku(int id);
        void RemoveSkuImages(List<int> ids);
        DtoSku SaveOrUpdate(DtoSku sku);
        void UpdateView(DtoProduct product);
    }
}