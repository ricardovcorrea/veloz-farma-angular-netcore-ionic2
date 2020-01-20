using IhChegou.Dto;
using IhChegou.DTO.Client;
using IhChegou.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.Order
{
    public class DtoOrder
    {
        public int Id { get; set; }
        public List<DtoSku> SkusRequesteds { get; set; }
        public bool AgreesReference { get; set; }
        public bool AgreesSimilar { get; set; }
        public bool AgressGeneric { get; set; }
        public List<DtoOrderResponse> Responses { get; set; }
        public DateTime? RequestedOn { get; set; }
        public DateTime? ScheduledTo { get; set; }
        public DateTime? DeliveredOn { get; set; }
        public DtoPaymentMethod SelectedPayment { get; set; }
        public DtoDelivery SelectedDelivery { get; set; }
        public DtoAddress AddressToShip { get; set; }
        public DtoClient Client { get; set; }
        public List<DtoStore> RequestedStores { get; set; }
    }
}
