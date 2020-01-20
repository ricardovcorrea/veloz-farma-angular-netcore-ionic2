using Dapper;
using IhChegou.DTO.Product;
using IhChegou.DTO.Search;
using IhChegou.Repository.Model.Parser.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IhChegou.Repository.Pharma.Model.Products;
using IhChegou.Repository.Contract;

namespace IhChegou.Repository.Pharma.Database
{
    public class ProductRepository : RepositoryBase, IProductRepository
    {
        public ProductRepository(IDatabaseSession session) : base(session)
        {
        }

        public DtoSku GetSkuByName(string name)
        {
            var dbSku = GetByKey<Sku>("Name", name, "sku");
            return dbSku.ToDTO();
        }

        public DtoProduct GetProductByNameAndProducer(string name, DtoProducer producer)
        {
            var sql = $"SELECT * FROM Product" +
                   $" WHERE Name ='{name}'" +
                   $" AND Producer_Id = {producer.Id}";

            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                var product = con.Query<Product>(sql).SingleOrDefault();

                return FillProduct(product);
            }

        }

        public List<DtoProduct> GetProductOrderByViews(int page, int size)
        {
            var query = @"SELECT * FROM Product order by Views desc ";
            query += $" limit {size}";
            query += $" offset {page * size}";

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<Product>(query);
                return FillProduct(result?.ToList());
            }
        }

        public List<DtoProduct> GetProductLike(string name, int page = 0, int size = 50)
        {
            var query = $"SELECT * FROM Product where Name like '%{name}%' order by Views desc ";
            query += $" limit {size}";
            query += $" offset {page * size}";

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<Product>(query);
                return FillProduct(result?.ToList());
            }
        }

        public DtoProduct GetProductBySku(int id)
        {
            var sql =
                $"SELECT " +
                $"    Product.* " +
                $" FROM " +
                $"    Sku," +
                $"    Product" +
                $" WHERE" +
                $"    Product.Id = Sku.Product_Id" +
                $"        AND Sku.Id = {id}";

            using (var con = SessionManager.GetConnection())
            {
                con.Open();
                var product = con.Query<Product>(sql).SingleOrDefault();

                if (product == null)
                    return null;
                return FillProduct(product);
            }
        }

        public DtoProducer GetProducerById(int id)
        {
            var producer = GetById<Producer>(id, "Producer");
            return producer.ToDTO();
        }


        private DtoProduct FillProduct(Product product)
        {
            if (product == null)
                return null;
            var dto = product.ToDTO();
            if (product?.Producer_Id != null)
                dto.Producer = GetProducerById(product.Producer_Id.Value);
            dto.Skus = GetSkuByProductId(product.Id);
            dto.Categories = GetCategoryByProductId(product.Id);
            dto.Principles = GetPrnciplesByProductId(product.Id);
            return dto;
        }

        private List<DtoProduct> FillProduct(List<Product> product)
        {
            return product?.Select(i => FillProduct(i)).ToList();
        }



        private DtoSku FillSku(Sku sku)
        {
            var dto = sku.ToDTO();
            var prod = GetProductBySku(sku.Id);
            dto.ProductReference = prod;
            return dto;
        }

        private DtoCategory FillCategory(Category category)
        {
            var dto = category.ToDTO();
            dto.SubCategories = GetSubCategories(category.Id);
            if (category.Category_id != null)
                dto.RefCategory = GetRefCategorie(category.Category_id.Value);
            return dto;
        }













        public DtoProduct GetProductById(int id)
        {
            var product = GetById<Product>(id, "Product");
            var dtoProduct = product.ToDTO();
            return dtoProduct;

        }

        public DtoSku GetSku(int id)
        {
            var sku = GetById<Sku>(id, "Sku");
            return FillSku(sku);
        }

        // size null is getAll
        public List<DtoSku> GetAllSkus(int page = 0, int size = 50)
        {
            var sku = GetAll<Sku>("Sku", page * size, size);
            return sku.Select(i => i.ToDTO())?.ToList();
        }

        public List<DtoSku> GetAllSkusWithoutImage(int page = 0, int size = 50)
        {
            var query = "select * from Sku where Image is not null";
            query += $" limit {size}";
            query += $" offset {page * size}";

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<DtoSku>(query);
                return result.ToList();
            }
        }

        public void RemoveSkuImages(List<int> ids)
        {
            var sql = $"update Sku set Image = null where Id in ({string.Join(", ", ids.ToArray())})";
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Execute(sql);
            }
        }

        public List<DtoSku> GetSkuByProductId(int id)
        {
            var sku = GetByKeyAll<Sku>("Product_id", id, "Sku");
            return sku.ToDTO();
        }

        #region GetAll

        public List<DtoProduct> GetAll(int offset = 0, int? size = null)
        {
            var products = GetAll<Product>("Product");
            return products.Select(i => FillProduct(i)).ToList();
        }
        public List<DtoSearch> GetAllSearchProducts()
        {
            var products = GetAll<Search>("Search");
            return products.ToDTO();
        }
        public List<DtoSearch> GetAllSearchProducts(int offset = 0, int size = 50)
        {
            var products = GetAll<Search>("Search", offset, size);
            return products.ToDTO();
        }
        public List<string> GetAllSearchCategories()
        {
            var sql = @"select distinct Name from Category";
            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<string>(sql);
                return result.ToList();
            }
        }
        public List<DtoProducer> GetAllProducers()
        {
            var producers = GetAll<Producer>("Producer");
            return producers.ToDTO();
        }
        public List<DtoCategory> GetAllCategories()
        {
            var category = GetAll<Category>("Category");
            return category.ToDTO();
        }
        public DtoCategory GetCategory(string name)
        {
            var result = GetByKey<Category>("Name", name, "Category");
            return result.ToDTO();
        }
        public List<DtoCategory> GetCategoryByProductId(int Id)
        {
            var query = @"SELECT 
                            Category.*
                        FROM
                            Category,
                            CategoryToProduct,
                            Product
                        WHERE
                            Product.Id = CategoryToProduct.Product_id
                                AND Category.Id = CategoryToProduct.Category_id
                                AND Product.Id = " + Id;

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<Category>(query);

                return result?.Select(i => FillCategory(i))?.ToList();
            }
        }


        private List<DtoCategory> GetSubCategories(int categoryId)
        {
            var dbCategories = GetByKeyAll<Category>("Category_id", categoryId, "Category");

            if (dbCategories != null)
            {
                var categories = dbCategories.Select(i => FillCategory(i)).ToList();

                return categories;
            }
            return null;
        }
        private DtoCategory GetRefCategorie(int categoryId)
        {
            var dbCategory = GetByKey<Category>("Category_id", categoryId, "Category");
            return dbCategory.ToDTO();
        }
        public List<DtoPrinciple> GetAllPrnciples()
        {
            var principle = GetAll<Principle>("Principle");
            return principle.ToDTO();
        }

        public List<DtoPrinciple> GetPrnciplesByProductId(int Id)
        {
            var query = @"SELECT 
                            Principle.*
                        FROM
                            Principle,
                            PrincipleToProduct,
                            Product
                        WHERE
                            Product.Id = PrincipleToProduct.Product_id
                                AND Principle.Id = PrincipleToProduct.Principle_id
                                AND Product.Id = " + Id;

            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();
                var result = connecton.Query<Principle>(query);

                return result?.Select(i => i.ToDTO())?.ToList();
            }
        }
        #endregion
        #region Save
        public void Save(ref DtoCategory category)
        {
            var repo = category.ToRepository();

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Category");
            }
            else
                Update(repo, "Category");

            List<DtoCategory> subcategories;

            if (category.SubCategories != null)
            {
                subcategories = new List<DtoCategory>();
                foreach (var subcCategory in category.SubCategories)
                {
                    var dbSubCategory = subcCategory.ToRepository();
                    dbSubCategory.Category_id = repo.Id;
                    if (dbSubCategory.Id == 0)
                    {
                        dbSubCategory.Id = Insert(dbSubCategory, "Category");
                    }
                    else
                        Update(dbSubCategory, "Category");
                    subcategories.Add(FillCategory(dbSubCategory));
                }
                category.SubCategories = subcategories;
            }
            category = FillCategory(repo);
        }

        public IList<DtoCategory> Save(IList<DtoCategory> category)
        {
            var updateCategories = new List<DtoCategory>();

            foreach (var item in category)
            {
                var repo = item.ToRepository();
                if (repo.Id == 0)
                {
                    repo.Id = Insert(repo, "Category");
                }
                else
                    Update(repo, "Category");

                updateCategories.Add(FillCategory(repo));
            }
            return updateCategories;

        }
        public void Save(ref DtoProducer producer)
        {
            var repo = producer.ToRepository();

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Producer");
            }
            else
                Update(repo, "Producer");

            producer = repo.ToDTO();
        }
        public DtoProducer Save(DtoProducer producer)
        {
            var repo = producer.ToRepository();

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Producer");
            }
            else
                Update(repo, "Producer");

            return repo.ToDTO();
        }
        public void Save(ref DtoPrinciple principle)
        {
            var repo = principle.ToRepository();

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Principle");
            }
            else
                Update(repo, "Principle");

            principle = repo.ToDTO();

        }
        public IList<DtoPrinciple> Save(IList<DtoPrinciple> category)
        {
            var updateCategories = new List<DtoPrinciple>();

            foreach (var item in category)
            {
                var repo = item.ToRepository();
                if (repo.Id == 0)
                {
                    repo.Id = Insert(repo, "Principle");
                }
                else
                    Update(repo, "Principle");

                updateCategories.Add(repo.ToDTO());
            }
            return updateCategories;

        }
        public void Save(ref DtoSku sku)
        {
            var repo = sku.ToRepository();

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Sku");
            }
            else
                Update(repo, "Sku");

            sku = FillSku(repo);

        }
        public IList<DtoSku> Save(IList<DtoSku> sku)
        {
            var updatedSku = new List<DtoSku>();

            foreach (var item in sku)
            {
                var repo = item.ToRepository();
                if (repo.Id == 0)
                {
                    repo.Id = Insert(repo, "Sku");
                }
                else
                    Update(repo, "Sku");

                updatedSku.Add(repo.ToDTO());
            }
            return updatedSku;

        }
        public void Save(ref DtoProduct product)
        {
            var repo = product.ToRepository();

            if (product.Producer != null)
            {
                product.Producer = Save(product.Producer);
                repo.Producer_Id = product.Producer.Id;
            }

            if (repo.Id == 0)
            {
                repo.Id = Insert(repo, "Product");
            }
            else
                Update(repo, "Product");


            if (product.Categories != null && product.Categories?.Count > 0)
            {
                product.Categories = Save(product.Categories);
                if (product.Categories != null)
                    foreach (var item in product.Categories)
                    {
                        InsertFk("Product_id", product.Id, "Category_id", item.Id, "CategoryToProduct");
                    }
            }

            if (product.Skus != null && product.Skus?.Count > 0)
            {
                foreach (var sku in product.Skus)
                {
                    sku.ProductReference = product;
                }
                product.Skus = Save(product.Skus);
            }

            if (product.Principles != null && product.Principles?.Count > 0)
            {
                product.Principles = Save(product.Principles);
                if (product.Principles != null)
                    foreach (var item in product.Principles)
                    {
                        InsertFk("Product_id", product.Id, "Principle_id", item.Id, "PrincipleToProduct");
                    }
            }




            product = FillProduct(repo);

        }
        #endregion
    }
}
