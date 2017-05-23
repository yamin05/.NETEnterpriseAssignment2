using System;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using WebApplication2.Exceptions;

namespace WebApplication2.Helpers
{
    public class ChangePasswordHelper
    {
        public string ChangePasswordFun(String UserId, String CurrentPassword, String NewPassword)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var findUser = userManager.FindById(UserId);
            if (findUser != null)
            {
                var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
                var result = userManager.ChangePassword(UserId, CurrentPassword, NewPassword);
                authenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false });
                if (result.Succeeded)
                {
                    var roles = userManager.GetRoles(findUser.Id);
                    return Utils.getInstance.getHomePageURL((Roles)Enum.Parse(typeof(Roles), roles[0]));
                }
                else
                {
                    throw new WrongCurrentPassword();
                } 
            }
            else
            {
                throw new WrongUserInputException();
            }
        }
    }
}