
namespace IhChegou.Repository.Pharma.Model.Orders
{
   public class DtoDelivery
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual bool Scheduled { get; set; }
    }
}
