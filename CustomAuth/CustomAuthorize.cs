using System.Web.Mvc;

namespace CustomAuth
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute 
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new ViewResult{ ViewName = "Unauthorized" };
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
        }
    }
}