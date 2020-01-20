using IhChegou.Global.Enumerators;

namespace IhChegou.Repository.Pharma.Model.Products
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
