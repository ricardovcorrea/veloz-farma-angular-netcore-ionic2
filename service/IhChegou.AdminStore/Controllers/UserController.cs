using IhChegou.Cache.Contract;
using IhChegou.Domain.Contract;
using IhChegou.Dto;
using IhChegou.DTO.User;
using IhChegou.WebApi.Extensions.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IhChegou.AdminStore.Controllers
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
            }
            return user;
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
    }
}
