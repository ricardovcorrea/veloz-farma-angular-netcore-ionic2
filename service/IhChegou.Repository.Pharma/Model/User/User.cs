using IhChegou.DTO;
using IhChegou.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Pharma.Model.User
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Document { get; set; }

        protected User()
        {

        }
        public User(DtoUser user)
        {
            this.Document = user.Document;
            this.Email = user.Email;
            this.Id = user.Id;
            this.Name = user.Name;
            this.PasswordHash = user.PasswordHash;
        }

        public DtoUser ToDto()
        {
            return new DtoUser()
            {
                Document = this.Document,
                Email = this.Email,
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
