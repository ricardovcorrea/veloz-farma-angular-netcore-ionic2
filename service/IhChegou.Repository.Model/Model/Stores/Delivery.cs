namespace IhChegou.Repository.Model.Stores
{
    public class Delivery
    {
        public virtual int Id { get; set; }
        public virtual decimal Price { get; set; }
        public virtual int MaxDistance { get; set; }
    }
}