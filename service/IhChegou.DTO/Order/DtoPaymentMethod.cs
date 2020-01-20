using IhChegou.Global.Enumerators;

namespace IhChegou.DTO.Order
{
    public class DtoPaymentMethod
    {
        public int Id { get; set; }
        public PaymentMethodType Type { get; set; }
    }
}