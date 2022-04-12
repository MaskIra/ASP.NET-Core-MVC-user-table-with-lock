using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebForUsers.Models;

namespace WebForUsers
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly ApplicationContext context;

        public CustomCookieAuthenticationEvents(ApplicationContext context)
        {
            this.context = context;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            string userId = (from c in context.Principal.Claims
                               where c.Type == ClaimTypes.NameIdentifier
                               select c.Value).FirstOrDefault();
            User user = this.context.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
            if(user == null || user.StatusId == 1)
            {
                context.RejectPrincipal();
                await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
