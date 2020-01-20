using IhChegou.Dto;
using IhChegou.DTO.User;
using IhChegou.Global.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Pharma.Model.User
{
    public class Role
    {
        public int Id { get; set; }
        public RoleType RoleName { get; set; }
        public int? User_id { get; set; }
        public int? Store_id { get; set; }

        public Role(DtoRole i, int UserId)
        {
            this.Id = i.Id;
            this.RoleName = i.Role;
            this.User_id = UserId;
        }

        public DtoRole ToDto(DtoStore store)
        {
            return new DtoRole()
            {
                Id = this.Id,
                Role = this.RoleName,
                Store = store
            };
        }

        internal static IEnumerable<Role> FromDto(IEnumerable<DtoRole> roles, int userId)
        {
            return roles.Select(i => new Role(i, userId));
        }
    }
}
