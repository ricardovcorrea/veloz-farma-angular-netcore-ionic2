using IhChegou.Dto;
using System.Collections.Generic;

namespace IhChegou.DTO.Order
{
    public class DtoOrderResponse
    {
        public virtual int Id { get; set; }
        public virtual IList<DtoSkuReply> SkuReplys { get; set; }
        public virtual DtoStore DrugStore { get; set; }
        public virtual bool Accepted { get; set; }
    }
}