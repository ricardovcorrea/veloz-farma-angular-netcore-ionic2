using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebTools.Models.Products
{
    public class RemoveImageModel
    {
        public int Id { get; set; }
        public int Page { get; set; }
        public bool ToDelete { get; set; }
    }
}
