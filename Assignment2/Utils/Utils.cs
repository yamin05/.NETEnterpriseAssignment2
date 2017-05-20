using Assignment2.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.Exceptions;

namespace Assignment2
{
    public sealed class Utils
    {
        private static readonly Utils instance = new Utils();

        private Utils() { }

        public static Utils getInstance
        {
            get
            {
                return instance;
            }
        }
        public void validateEmail(string inputEmail)
        {
            if (isNullOrEmpty(inputEmail))
            {
                throw new ValueIsNullException();
            }
            Regex regex = new Regex(@"\w + ([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
            Match match = regex.Match(inputEmail);
            if (!match.Success)
            {
                throw new InvalidEmailFormatException();
            }
        }

        public void validatePassword(string inputPassword)
        {
            if (isNullOrEmpty(inputPassword))
            {
                throw new ValueIsNullException();
            }
            Regex regex = new Regex(@"\w{4,8}");
            Match match = regex.Match(inputPassword);
            if (!match.Success)
            {
                throw new InvalidEmailFormatException();
            }
        }

        public bool isNullOrEmpty<T>(T value)
        {
            if (typeof(T) == typeof(string))
                return string.IsNullOrEmpty(value as string);

            return value == null || value.Equals(default(T));
        }

        public string getHomePageURL(Roles role)
        {
            return role + "/" + role + "Home.aspx";
        }

        public string getHomePageURL()
        {
            var role = GetCurrentUserRole();
            return "~/Views/" + role + "/" + role + "Home.aspx";
        }

        public string GetCurrentUserRole()
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            var roles = userManager.GetRoles(GetCurrentUserId());
            return roles.FirstOrDefault();
        }

        public string GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId();
        }

        public string GetDistrictName(string district)
        {
            return district.Replace("_", " ");
        }
    }
}