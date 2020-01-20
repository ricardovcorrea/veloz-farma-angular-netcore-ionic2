using IhChegou.Global.Enumerators;
using System.Collections.Generic;

namespace IhChegou.DTO.Product
{
    public class DtoProduct
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Serving { get; set; }
        public virtual ProductType Type { get; set; }
        public virtual bool NeedRecipe { get; set; }
        public virtual IList<DtoCategory> Categories { get; set; }
        public virtual DtoProducer Producer { get; set; }
        public virtual IList<DtoPrinciple> Principles { get; set; }
        public virtual IList<DtoSku> Skus { get; set; }
        public int Views { get; set; }
    }
}