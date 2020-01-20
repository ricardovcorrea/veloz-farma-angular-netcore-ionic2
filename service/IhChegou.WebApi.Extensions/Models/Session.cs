using IhChegou.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IhChegou.WebApi.Extensions
{
    public class Session
    {
        public TimeSpan TTL { get; set; } = TimeSpan.FromDays(1.5);
        public string SessionId { get; set; }
        public string ClientToken { get; set; }
        public int OrderId { get; set; }
        public int LastSelectedAddress { get; set; }
        public bool MobileAuthenticate { get; set; }



        public int? StoreId { get; set; }

        public bool AdminAuthenticate { get; set; }
        public int UserId { get; set; }
        public IEnumerable<DtoRole> Roles { get; set; }
    }
}