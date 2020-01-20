using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IhChegou.DTO;
using IhChegou.Repository.Contract;
using IhChegou.Repository.Pharma.Model.User;
using IhChegou.DTO.User;
using IhChegou.DTO.Common;
using IhChegou.Global.Extensions.IList;
using Dapper;

namespace IhChegou.Repository.Pharma.Database
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly IStoreRepository storeRepository;

        public UserRepository(IDatabaseSession session, IStoreRepository storeRepository) : base(session)
        {
            this.storeRepository = storeRepository;
        }

        public DtoUser GetClient(string email, string passwordHash)
        {
            var User = GetByKey<User>("Email", email, "User");
            if (User?.PasswordHash == passwordHash)
                return FillUser(User);
            else
                return null;
        }

        public void Create(DtoUser user)
        {
            User model = new User(user);
            model.Id = Insert(model, "User");
            if (user.Roles?.Count() > 0)
            {
                var roles = Role.FromDto(user.Roles, model.Id);
                Insert(roles, "Role");
            }

        }


        public IEnumerable<DtoRole> GetRoles(int UserId)
        {
            var roles = GetByKeyAll<Role>("User_id", UserId, "Role");
            var stores = storeRepository.Get(roles.Where(i => i.Store_id != null).Select(i => i.Store_id.Value));
            return FillRole(roles, stores);
        }

        private static IEnumerable<DtoRole> FillRole(List<Role> roles, IEnumerable<Dto.DtoStore> stores)
        {
            var dtoRoles = new List<DtoRole>();
            foreach (var item in roles)
            {
                var dtostore = stores.Where(i => i.Id == item.Store_id).FirstOrDefault();
                var dtorole = item.ToDto(dtostore);

                dtoRoles.Add(dtorole);
            }
            return dtoRoles;
        }

        private DtoUser FillUser(User user)
        {
            var dto = user.ToDto();
            dto.Roles = GetRoles(user.Id);
            return dto;
        }

        public void AddRole(int clientId, IEnumerable<DtoRole> role)
        {
            var roles = Role.FromDto(role, clientId);
            Insert(roles, "Role");
        }

        public void RemoveRole(int clientId, IEnumerable<DtoRole> role)
        {
            var ClientRoles = GetRoles(clientId);
            var userRoleId = ClientRoles.Select(i => i.Id).Intersect(role.Select(i => i.Id));
            foreach (var item in userRoleId)
            {
                Delete(item, "Role");
            }
        }

        public new DtoPage<DtoUser> GetAllUsers(int page, int size)
        {
            var result = GetAll<User>("User", page * size, size);

            return new DtoPage<DtoUser>(result.Select(i => i.ToDto()), size, result.RowCount(), page);
        }

        public new DtoPage<DtoUser> GetAllUsers(int storeId, int size, int page)
        {
            var query = @"SELECT DISTINCT
                            User.Id, User.Email, User.Document, User.Name
                        FROM
                            User,
                            Role
                        WHERE
                            Role.Store_id = @StoreId and
                            User.Id = Role.User_id";

            SetPagination(query, size, page);

            

            var countQuery = @"SELECT DISTINCT
                          count(User.Id)
                        FROM
                            User,
                            Role
                        WHERE
                            Role.Store_id = @StoreId and
                            User.Id = Role.User_id";



            using (var connecton = SessionManager.GetConnection())
            {
                connecton.Open();

                var result = connecton.Query<User>(query, new { storeId = storeId }).ToList();
                var count = connecton.Query<int>(countQuery, new { storeId = storeId }).Single();

                result.SetRowCount(count);

                return new DtoPage<DtoUser>(result.Select(i => i.ToDto()), size, result.RowCount(), page);
            }
        }
    }
}
