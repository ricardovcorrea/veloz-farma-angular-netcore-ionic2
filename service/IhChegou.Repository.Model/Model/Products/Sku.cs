using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Products
{
    public class Sku
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Image { get; set; }
        public virtual int Views { get; set; }

        public int? Product_Id { get; set; }
    }

    public class SkuOrder : Sku
    {
        public virtual int? Quantity { get; set; }
        public virtual int? Orderskurequest_Id { get; set; }
    }
}
