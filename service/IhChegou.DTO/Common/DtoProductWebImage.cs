using IhChegou.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.Common
{
    public class DtoProductWebImage
    {
        public DtoProduct Product { get; set; }
        public List<string> Images { get; set; }
    }
}
