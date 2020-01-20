using IhChegou.Global.Enumerators;

namespace IhChegou.Repository.Pharma.Model.Stores
{
     public class PaymentMethod
    {
        public virtual int Id { get; set; }
        public virtual PaymentMethodType Type { get; set; }
    }
}