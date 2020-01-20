using IhChegou.DTO.Order;
using IhChegou.Global.Extensions.String;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IhChegou.DTO.Client
{
    public class DtoClient
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public string Token { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        [JsonIgnore]
        public string PasswordHash => Password == null ? null : Password.EncryptSHA256();

        public string Document { get; set; }
        public List<DtoAddress> Addresses { get; set; }
        public DtoOrder AtualOrder { get; set; }
        public string DeviceId { get; set; }
    }
}