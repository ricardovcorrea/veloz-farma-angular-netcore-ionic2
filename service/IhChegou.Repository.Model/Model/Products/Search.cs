using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Products
{
    public class Search
    {
        public virtual int ProductId { get; set; }
        public virtual string Product { get; set; }
        public virtual string Serving { get; set; }
        public virtual string SkuName { get; set; }
        public virtual int Views { get; set; }
        public virtual string Producer { get; set; }
        public virtual string Categories { get; set; }
        public virtual string Principles { get; set; }
        public virtual string Image { get; set; }
        public virtual int SkuId { get; set; }
    }
}
