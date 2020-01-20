using IhChegou.DTO;
using IhChegou.DTO.Common;
using IhChegou.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IhChegou.Repository.Contract
{
    public interface IUserRepository
    {
        DtoUser GetClient(string email, string pass);

        void Create(DtoUser user);
        void AddRole(int clientId, IEnumerable<DtoRole> role);
        void RemoveRole(int clientId, IEnumerable<DtoRole> role);
        IEnumerable<DtoRole> GetRoles(int userId);
        DtoPage<DtoUser> GetAllUsers(int size, int page);
        DtoPage<DtoUser> GetAllUsers(int storeId, int size, int page);
    }
}
