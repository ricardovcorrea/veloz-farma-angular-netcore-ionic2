using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net.Http.Headers;
using IhChegou.WebApi.Extensions.API;

namespace IhChegou.WebApi.Extensions
{
    public class CustomActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as IhChegouController;
            controller.GetSession();

            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as IhChegouController;
            controller.UpdateSession();
            if (actionExecutedContext.Response != null)
            {
                var session = controller.Session.SessionId;
                var ttl = DateTime.Now.Add(controller.Session.TTL);
                actionExecutedContext.Response.Headers.Clear();
                actionExecutedContext.Response.Headers.Add("Session", session);
                actionExecutedContext.Response.Headers.Add("TTL", ttl.ToString());
                actionExecutedContext.Response.Headers.Add("Access-Control-Expose-Headers", "Session");
                actionExecutedContext.Response.Headers.Add("Access-Control-Expose-Headers", "TTL");

                CookieHeaderValue cookie = actionExecutedContext.Request.Headers.GetCookies("SessionCookie")?.FirstOrDefault();
                if (cookie == null || cookie["SessionCookie"].Value != session)
                {
                    var setCookie = new CookieHeaderValue("SessionCookie", session);
                    setCookie.Path = "/";
                    actionExecutedContext.Response.Headers.AddCookies(new CookieHeaderValue[] { setCookie });
                }
            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}