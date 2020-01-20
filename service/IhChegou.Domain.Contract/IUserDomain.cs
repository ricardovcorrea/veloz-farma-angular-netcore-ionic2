using IhChegou.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IhChegou.DTO.Common;

namespace IhChegou.Domain.Contract
{
    public interface IUserDomain
    {
        DtoUser Login(string email, string password);
        void Create(DtoUser user);
        void AddRole(int clientId, IEnumerable<DtoRole> role);
        void RemoveRole(int id, IEnumerable<DtoRole> role);
        IEnumerable<DtoRole> GetRoles(int userId);
        DtoPage<DtoUser> GetAll(int storeId, int size, int page);
        DtoPage<DtoUser> GetAll(int size, int page);
    }
}
