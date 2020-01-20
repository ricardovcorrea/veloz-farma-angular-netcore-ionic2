using IhChegou.Domain.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IhChegou.DTO.User;
using IhChegou.Repository.Contract;
using IhChegou.Global.Extensions.String;
using IhChegou.DTO.Common;

namespace IhChegou.Domain.Pharma.User
{
    public class UserDomain : IUserDomain
    {
        public IUserRepository UserRepository { get; }

        public UserDomain(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }


        public void Create(DtoUser user)
        {
            user.PasswordHash = user.Password.EncryptSHA256();
            UserRepository.Create(user);
        }

        public DtoUser Login(string email, string password)
        {
           return  UserRepository.GetClient(email, password.EncryptSHA256());
        }

        public void AddRole(int clientId, IEnumerable<DtoRole> role)
        {
            UserRepository.AddRole(clientId, role);
        }

        public void RemoveRole(int id, IEnumerable<DtoRole> role)
        {
            UserRepository.RemoveRole(id, role);
        }

        public IEnumerable<DtoRole> GetRoles(int userId)
        {
           return UserRepository.GetRoles(userId);

        }

        public DtoPage<DtoUser> GetAll(int storeId, int size, int page)
        {
            return UserRepository.GetAllUsers(storeId,size,page);

        }

        public DtoPage<DtoUser> GetAll(int size, int page)
        {
            return UserRepository.GetAllUsers(size, page);

        }
    }
}
