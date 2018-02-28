using System.Runtime.Remoting.Channels;
using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace CustomAuth
{
    public class CustomAuthenticationAttribute: FilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var authCookie = filterContext.HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                if (!filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)
                    && !filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute),
                        inherit: true))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login"})); ;
                }

                return;
            }

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            var identity = new GenericIdentity(authTicket.Name);
            var principal = new GenericPrincipal(identity, new[] { authTicket.UserData });

            filterContext.HttpContext.User = principal;
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
        }
    }
}