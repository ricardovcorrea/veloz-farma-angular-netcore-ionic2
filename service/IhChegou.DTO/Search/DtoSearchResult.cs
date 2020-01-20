using System;
using System.Collections.Generic;

namespace IhChegou.DTO.Search
{
    public class DtoSearchResult
    {
        public string QueryTime { get; set; }
        public DtoSearchRequest Request { get; set; }
        public List<DtoSearch> Result { get; set; }
        public long Total { get; set; }
    }
}