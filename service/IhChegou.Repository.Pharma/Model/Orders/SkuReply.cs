

namespace IhChegou.Repository.Pharma.Model.Orders
{
     public class SkuReply
    {
        public virtual int Id { get; set; }
        public virtual int? OriginalSku_id { get; set; }
        public virtual int? OferredSku_id { get; set; }
        public virtual int? OrderResponse_id { get; set; }
        public virtual decimal? Price { get; set; }
        public virtual bool OutStock { get; set; }
    }
}