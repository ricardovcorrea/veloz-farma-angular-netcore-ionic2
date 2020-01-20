

namespace IhChegou.Repository.Pharma.Model.Products
{
    public class Sku
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Image { get; set; }
        public virtual int Views { get; set; }

        public int? Product_Id { get; set; }
    }

 
}
