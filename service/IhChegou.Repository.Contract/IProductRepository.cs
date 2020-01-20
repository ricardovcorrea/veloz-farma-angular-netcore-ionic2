using System.Collections.Generic;
using IhChegou.DTO.Product;
using IhChegou.DTO.Search;

namespace IhChegou.Repository.Pharma.Database
{
    public interface IProductRepository
    {
        List<DtoProduct> GetAll(int offset = 0, int? size = default(int?));
        List<DtoCategory> GetAllCategories();
        List<DtoPrinciple> GetAllPrnciples();
        List<DtoProducer> GetAllProducers();
        List<string> GetAllSearchCategories();
        List<DtoSearch> GetAllSearchProducts();
        List<DtoSearch> GetAllSearchProducts(int offset = 0, int size = 50);
        List<DtoSku> GetAllSkus(int page = 0, int size = 50);
        List<DtoSku> GetAllSkusWithoutImage(int page = 0, int size = 50);
        DtoCategory GetCategory(string name);
        List<DtoCategory> GetCategoryByProductId(int Id);
        List<DtoPrinciple> GetPrnciplesByProductId(int Id);
        DtoProducer GetProducerById(int id);
        DtoProduct GetProductById(int id);
        DtoProduct GetProductByNameAndProducer(string name, DtoProducer producer);
        DtoProduct GetProductBySku(int id);
        List<DtoProduct> GetProductLike(string name, int page = 0, int size = 50);
        List<DtoProduct> GetProductOrderByViews(int page, int size);
        DtoSku GetSku(int id);
        DtoSku GetSkuByName(string name);
        List<DtoSku> GetSkuByProductId(int id);
        void RemoveSkuImages(List<int> ids);
        DtoProducer Save(DtoProducer producer);
        IList<DtoCategory> Save(IList<DtoCategory> category);
        IList<DtoPrinciple> Save(IList<DtoPrinciple> category);
        IList<DtoSku> Save(IList<DtoSku> sku);
        void Save(ref DtoCategory category);
        void Save(ref DtoPrinciple principle);
        void Save(ref DtoProducer producer);
        void Save(ref DtoProduct product);
        void Save(ref DtoSku sku);
    }
}