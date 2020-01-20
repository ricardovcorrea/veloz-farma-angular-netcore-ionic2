

using System;

namespace IhChegou.Repository.Pharma.Model.Orders
{
    public class Order
    {
        public virtual int Id { get; set; }
        public virtual bool AgreesReference { get; set; }
        public virtual bool AgreesSimilar { get; set; }
        public virtual bool AgressGeneric { get; set; }
        public virtual DateTime? RequestedOn { get; set; }
        public virtual DateTime? ScheduledTo { get; set; }
        public virtual DateTime? DeliveredOn { get; set; }

        public virtual int? Client_Id { get; set; }
        public virtual int? SelectedPayment_Id { get; set; }
        public virtual int? SelectedDelivery_Id { get; set; }
        public virtual int? AddressToShip_Id { get; set; }


        //public virtual IList<Store> RequestedStores { get; set; }
        //public virtual IList<OrderSkuRequest> SkusRequesteds { get; set; }
        //public virtual IList<OrderResponse> Responses { get; set; }


    }
}