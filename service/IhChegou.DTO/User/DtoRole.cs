using IhChegou.Dto;
using IhChegou.Global.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.DTO.User
{
    public class DtoRole
    {
        public int Id { get; set; }
        public RoleType Role { get; set; }
        public DtoStore Store { get; set; }
    }
}
