using System.Collections.Generic;

namespace IhChegou.Repository.Model.Products
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Category_id { get; set; }
    }
}