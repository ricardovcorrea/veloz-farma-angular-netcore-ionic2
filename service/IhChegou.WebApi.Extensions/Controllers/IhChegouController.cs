using IhChegou.Cache.Contract;
using IhChegou.Global.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace IhChegou.WebApi.Extensions.API
{
    [CustomActionFilter]
    public class IhChegouController : ApiController
    {
        public ICacheManager CacheManager;
        public IhChegouController(ICacheManager cache)
        {
            CacheManager = cache;
        }

        public Session Session { get; set; }

        internal void UpdateSession()
        {
            Session = Session ?? new Session();
            if (Session.SessionId == null)
                Session.SessionId = Guid.NewGuid().ToString();
            CacheManager.Set(CacheKey.Session, Session.SessionId, Session, Session.TTL);

        }

        public Session GetSession()
        {
            if (Session != null)
                return Session;
            SetSession();
            return Session;
        }

        private void SetSession()
        {
            Session session = null;

            var id = GetSessionFromHeader();

            if (id == null)
            {
                id = GetSessionFromCookie();
            }

            if (id != null)
                session = CacheManager.Get<Session>(CacheKey.Session, id);

            if (session == null)
                session = new Session()
                {
                    SessionId = Guid.NewGuid().ToString()
                };
            Session = session;
        }

        private string GetSessionFromHeader()
        {
            IEnumerable<string> headers;
            this.ActionContext.Request.Headers.TryGetValues("Session", out headers);
            return headers?.FirstOrDefault();
        }

        private string GetSessionFromCookie()
        {
            string id = null;
            CookieHeaderValue cookie = Request.Headers?.GetCookies("SessionCookie")?.FirstOrDefault();
            if (cookie != null)
            {
                id = cookie["SessionCookie"].Value;
            }
            return id;
        }
    }
}

