using System.Web.Mvc;
using CustomAuth.Models;

namespace CustomAuth.Controllers
{
    [CustomAuthentication]
    [CustomAuthorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(Roles = "ADMIN")]
        public ActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Login login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (!Security.Authenticate(login))
                {
                    ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
                    return View(login);
                }

                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index");
            }

            return View(login);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            Security.Logout();
            return RedirectToAction("Login");
        }
    }
}