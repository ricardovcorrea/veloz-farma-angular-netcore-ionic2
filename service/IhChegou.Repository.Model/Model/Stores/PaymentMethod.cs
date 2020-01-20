using IhChegou.Global.Enumerators;

namespace IhChegou.Repository.Model.Stores
{
     public class PaymentMethod
    {
        public virtual int Id { get; set; }
        public virtual PaymentMethodType Type { get; set; }

    }
}