using IhChegou.DTO.Product;

namespace IhChegou.DTO.Order
{
    public class DtoSkuReply
    {
        public int Id { get; set; }
        public DtoSku OriginalSku { get; set; }
        public DtoSku OferredSku { get; set; }
        public decimal? Price { get; set; }
        public bool OutStock { get; set; }
    }
}