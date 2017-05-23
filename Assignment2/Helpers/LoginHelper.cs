using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Web;
using WebApplication2.Exceptions;

namespace WebApplication2.Helpers
{
    public class LoginHelper
    {
        public string login(String inputUsername, String inputPassword)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var findUser = userManager.Find(inputUsername, inputPassword);
            if (findUser != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(findUser, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, userIdentity);
                var roles = userManager.GetRoles(findUser.Id);
                
                return Utils.getInstance.getHomePageURL((Roles) Enum.Parse(typeof(Roles), roles[0]));
            }  
            else
            {
                throw new WrongUserInputException();
            }
        }
    }
}