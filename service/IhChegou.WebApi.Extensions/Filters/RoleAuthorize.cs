using IhChegou.Global.Enumerators;
using IhChegou.WebApi.Extensions.API;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System;

namespace IhChegou.Admin.Filters
{
    public class RoleAuthorize : AuthorizeAttribute
    {
        private readonly RoleType[] roles;
        private TimeSpan TTL = TimeSpan.FromDays(1);

        public RoleAuthorize()
        {

        }

        public RoleAuthorize(params RoleType[] role)
        {
            this.roles = role;
        }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as IhChegouController;
            var session = controller.GetSession();
            if (!session.AdminAuthenticate)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                return;
            }

            if (roles?.Count() > 0)
            {
                bool Auth = false;
                foreach (var item in session.Roles)
                {
                    if (roles.Contains(item.Role))
                    {
                        Auth = true;
                        continue;
                    }
                }
                if (Auth == false)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                    return;
                }
            }

            session.TTL = TTL;
        }

    }
}