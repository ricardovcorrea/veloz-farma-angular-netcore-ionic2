using IhChegou.API.Controllers.API;
using IhChegou.WebApi.Extensions.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace IhChegou.API.Filters
{
    public class AuthorizeMobile : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as IhChegouController;
            var session = controller.GetSession();
            if (!session.MobileAuthenticate)
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}