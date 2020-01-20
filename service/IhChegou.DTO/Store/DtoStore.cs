using IhChegou.DTO.Client;
using IhChegou.DTO.Order;
using IhChegou.DTO.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Dto
{
    public class DtoStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float Profit { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double Proximity { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double MaxDistance { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DtoAddress Address { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DtoPaymentMethod> AvailablePayments { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<DtoDelivery> AvaibleDeliveries { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DtoDelivery Delivery { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable<DtoWorkDay> WorkingDays{ get; set; }
    }
}
