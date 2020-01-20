namespace IhChegou.DTO.Order
{
    public class DtoDelivery
    {
        public virtual int Id { get; set; }
        public virtual decimal Price { get; set; }
        public int MaxDistance { get; set; }
    }
}