using Newtonsoft.Json;
using System.Web;

namespace IhChegou.DTO.Product
{
    public class DtoSku
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }


        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual int? OrderSkuId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public virtual int? Quantity { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DtoProduct ProductReference { get; set; }

        public void SetProductReference(DtoProduct prod)
        {
            ProductReference = new DtoProduct()
            {
                Id = prod.Id,
                Name = prod.Name,
                Categories = prod.Categories,
                NeedRecipe = prod.NeedRecipe,
                Principles = prod.Principles,
                Producer = prod.Producer,
                Serving = prod.Serving,
                Type = prod.Type,
                Views = prod.Views
            };
            ProductReference.Skus?.Clear();
        }
    }
}