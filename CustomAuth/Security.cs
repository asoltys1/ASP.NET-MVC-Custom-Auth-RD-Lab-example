using System;
using System.Web;
using System.Web.Security;
using CustomAuth.Models;

namespace CustomAuth
{
    public static class Security
    {
        public static bool Authenticate(Login login)
        {
            if (!(login.Username == "user" && login.Password == "user" ||
                  login.Username == "admin" && login.Password == "admin"))
            {
                return false;
            }

            string role = login.Username.ToUpper();
            var ticket = new FormsAuthenticationTicket(1, login.Username, DateTime.Now, DateTime.Now.AddHours(24), true, role);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }

        public static void Logout()
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty){ Expires = new DateTime(1999, 10, 12) };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}