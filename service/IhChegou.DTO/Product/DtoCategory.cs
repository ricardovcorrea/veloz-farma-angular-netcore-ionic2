using System.Collections.Generic;

namespace IhChegou.DTO.Product
{
    public class DtoCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public List<DtoCategory> SubCategories { get; set; }
        public DtoCategory RefCategory { get; set; }
    }
}