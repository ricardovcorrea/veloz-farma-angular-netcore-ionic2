using IhChegou.Domain.Contract.Product;
using IhChegou.DTO.Product;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Database;
using System.Collections.Generic;
using System;

namespace IhChegou.Domain.Pharma.Product
{
    public class ProductDomain : IProductDomain
    {
        private IProductRepository ProductRepository { get; set; }
        private ISearchRepository SearchRepository;
        public ProductDomain(IProductRepository poductRepository, ISearchRepository searchRepository)
        {
            ProductRepository = poductRepository;
            SearchRepository = searchRepository;
        }

        public DtoProduct GetProduct(int id)
        {
            var result = ProductRepository.GetProductById(id);
            result.Views++;

            SearchRepository.UpdateRanking(id, result.Views);

            ProductRepository.Save(ref result);
            return result;
        }
        public List<DtoProduct> GetProductOrderByRank(int page, int size)
        {
            return ProductRepository.GetProductOrderByViews(page, size);
        }

        public List<DtoProduct> GetProductLike(string name, int page, int size)
        {
            return ProductRepository.GetProductLike(name, page, size);
        }
        public DtoProduct GetProductBySku(int id)
        {
            var result = ProductRepository.GetProductBySku(id);
            return result;
        }


        public DtoSku GetSku(int id)
        {
            var result = ProductRepository.GetSku(id);
            return result;
        }

        public List<DtoSku> GetAllSkus(int page = 0, int size = 50)
        {
            var result = ProductRepository.GetAllSkus(page, size);
            return result;
        }

        public List<DtoSku> GetAllSkusWithoutImage(int page = 0, int size = 50)
        {
            var result = ProductRepository.GetAllSkusWithoutImage(page, size);
            return result;
        }

        public void RemoveSkuImages(List<int> ids)
        {
            ProductRepository.RemoveSkuImages(ids);
        }

        public DtoSku SaveOrUpdate(DtoSku sku)
        {
            ProductRepository.Save(ref sku);
            return sku;
        }

        public void UpdateView(DtoProduct product)
        {
            var dbProduct = ProductRepository.GetProductById(product.Id);
            dbProduct.Views = product.Views;
            ProductRepository.Save(ref dbProduct);
        }
    }
}