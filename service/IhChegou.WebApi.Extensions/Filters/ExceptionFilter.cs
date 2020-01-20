using IhChegou.Global.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace IhChegou.WebApi.Extensions
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is BadRequest)
            {
                HttpResponseMessage response;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                var error = new Error(actionExecutedContext.Exception as BadRequest);
                response.Content = new ObjectContent<Error>(error, new JsonMediaTypeFormatter());
                actionExecutedContext.Response = response;
                actionExecutedContext.Response.Headers.Add("Error-Id", error.Code.ToString());
            }
            if (actionExecutedContext.Exception is UnauthorizedAccessException)
            {
                HttpResponseMessage response;
                response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                var error = new Error(actionExecutedContext.Exception as UnauthorizedAccessException);
                response.Content = new ObjectContent<Error>(error, new JsonMediaTypeFormatter());
                actionExecutedContext.Response = response;
                actionExecutedContext.Response.Headers.Add("Error-Id", error.Code.ToString());
            }
            if (actionExecutedContext.Exception is GenericError)
            {
                HttpResponseMessage response;
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                var error = new Error(actionExecutedContext.Exception as GenericError);
                response.Content = new ObjectContent<Error>(error, new JsonMediaTypeFormatter());
                actionExecutedContext.Response = response;
                actionExecutedContext.Response.Headers.Add("Error-Id", error.Code.ToString());
            }
            base.OnException(actionExecutedContext);

        }
    }
}