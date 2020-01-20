
namespace IhChegou.Repository.Pharma.Model.Products
{
    public class SkuOrder : Sku
    {
        public virtual int? Quantity { get; set; }
        public virtual int? Orderskurequest_Id { get; set; }
    }
}
