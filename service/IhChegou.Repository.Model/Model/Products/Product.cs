using IhChegou.Global.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Model.Products
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Serving { get; set; }
        public virtual ProductType Type { get; set; }
        public virtual bool NeedRecipe { get; set; }
        public virtual int Views { get; set; }

        public int? Producer_Id { get; set; }


    
    }
}
