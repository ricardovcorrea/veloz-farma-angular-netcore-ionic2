using IhChegou.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.User
{
    public class DtoUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string Document { get; set; }

        public IEnumerable<DtoRole> Roles { get; set; }

        public DtoStore AtualStore { get; set; }

    }
}
