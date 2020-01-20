

namespace IhChegou.Repository.Pharma.Model.Orders
{
     public class OrderResponse
    {
        public virtual int Id { get; set; }
        public virtual int DrugStore_id { get; set; }
        public virtual int Order_id { get; set; }
        public virtual bool Accepted { get; set; }
    }
}