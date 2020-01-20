using IhChegou.Admin.Filters;
using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract;
using IhChegou.Dto;
using IhChegou.DTO.Common;
using IhChegou.DTO.User;
using IhChegou.Global.Enumerators;
using IhChegou.WebApi.Extensions;
using IhChegou.WebApi.Extensions.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.Admin.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : IhChegouController
    {
        public IUserDomain UserDomain { get; }

        public UserController(IUserDomain userDomain, ICacheManager cache) : base(cache)
        {
            UserDomain = userDomain;
        }


        [Route("user/login")]
        [HttpGet]
        public DtoUser Login(string email, string password)
        {
            var user = UserDomain.Login(email, password);
            if (user != null)
            {
                Session.UserId = user.Id;
                Session.AdminAuthenticate = true;

                var storeRole = user.Roles.GroupBy(i => i.Store).FirstOrDefault();
                var store = storeRole.Key;
                var roles = storeRole.ToList();

                Session.Roles = roles;
                Session.StoreId = store?.Id;

                user.AtualStore = store;
                return user;
            }
            throw new UnauthorizedAccessException("Wrong Password");

        }

        [Route("user/changeStore/{storeId}")]
        [HttpPatch]
        public DtoStore ChangeStore(int storeId)
        {
            var session = GetSession();

            var userRoles = UserDomain.GetRoles(Session.UserId);

            var storeRole = userRoles.GroupBy(i => i.Store).FirstOrDefault();
            var store = storeRole.Key;
            var roles = storeRole.ToList();

            Session.Roles = roles;
            Session.StoreId = store?.Id;

            if (store == null)
            {
                throw new UnauthorizedAccessException("Forbidden for store: " + storeId);
            }

            session.StoreId = store.Id;
            return store;
        }

        [Route("user")]
        [HttpPost]
        [RoleAuthorize(RoleType.God, RoleType.StoreAdmin)]
        public void Post([FromBody]DtoUser value)
        {
            var session = GetSession();
            if (session.StoreId.HasValue)
            {
                foreach (var item in value.Roles)
                {
                    item.Store = new DtoStore
                    {
                        Id = session.StoreId.Value
                    };
                }
            }
            UserDomain.Create(value);
        }

        [Route("users")]
        [HttpGet]
        [RoleAuthorize(RoleType.God, RoleType.StoreAdmin)]
        public DtoPage<DtoUser> GetUsers(int page, int size)
        {
            var session = GetSession();

            if (session.StoreId == null)
            {
                return UserDomain.GetAll(size,page);
            }
            else
                return UserDomain.GetAll(session.StoreId.Value,size,page);
        }

        [Route("user/{id}/role")]
        [HttpPost]
        [RoleAuthorize(RoleType.God)]
        public void AddRole(int id, IEnumerable<DtoRole> role)
        {
            UserDomain.AddRole(id, role);
        }

        [Route("user/role")]
        [HttpPost]
        [RoleAuthorize(RoleType.StoreAdmin)]
        public void AddRoleStore(IEnumerable<DtoRole> role)
        {
            var session = GetSession();
            UserDomain.AddRole(session.StoreId.Value, role);
        }

        [Route("user/{id}/role")]
        [HttpDelete]
        [RoleAuthorize(RoleType.God)]
        public void RemoveRole(int id, IEnumerable<DtoRole> role)
        {
            UserDomain.RemoveRole(id, role);
        }

        [Route("user/role")]
        [HttpDelete]
        [RoleAuthorize(RoleType.God)]
        public void RemoveRoleStore(IEnumerable<DtoRole> role)
        {
            var session = GetSession();
            UserDomain.RemoveRole(session.StoreId.Value, role);
        }







    }
}
