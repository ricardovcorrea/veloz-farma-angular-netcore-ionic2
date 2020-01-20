using System.Collections.Generic;

namespace IhChegou.DTO.Search
{
    public class DtoCategoryTreeItem
    {
        public string Name { get; set; }
        public List<DtoCategoryTreeItem> SubCategories { get; set; }
    }
}