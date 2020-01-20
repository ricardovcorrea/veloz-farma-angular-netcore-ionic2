using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Search.ElasticSearch.Model
{
    class SearchRequest
    {
        public int From { get; set; }
        public int Size { get; set; }
        public string Query { get; set; }

    }
}
