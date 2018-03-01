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
            var expires = DateTime.Now.AddMonths(1);
            var ticket = new FormsAuthenticationTicket(1, login.Username, DateTime.Now, expires, true, role);
            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            if (login.RememberMe)
            {
                cookie.Expires = expires;
            }
            HttpContext.Current.Response.Cookies.Add(cookie);

            return true;
        }

        public static void Logout()
        {
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, string.Empty){ Expires = DateTime.Now.AddYears(-10) };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}