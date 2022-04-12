using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using WebForUsers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace WebForUsers.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplicationContext context;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationContext db)
        {
            _logger = logger;
            this.context = db;
        }

        public IActionResult Index()
        {
            return View(context.Users.Include(u => u.Status).ToList());
        }

        [HttpPost]
        public IActionResult Block(int[] ids)
        {
            ChangUsersStatus(ids, true);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Unblock(int[] ids)
        {
            ChangUsersStatus(ids, false);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Delete(int[] ids)
        {
            DeleteUsers(ids);
            return RedirectToAction("Index", "Home");
        }

        private void ChangUsersStatus(int[] ids, bool status)
        {
            foreach (int id in ids)
            {
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                LogoutCurrentUser(user);
                user.Status = context.Statuses.FirstOrDefault(s => s.Name == (status ? "blocked" : "unblocked"));
            }
            context.SaveChanges();
        }
        private void DeleteUsers(int[] ids)
        {
            foreach (int id in ids)
            {
                User user = context.Users.FirstOrDefault(u => u.Id == id);
                LogoutCurrentUser(user);
                context.Users.Remove(user);
            }
            context.SaveChanges();
        }

        private void LogoutCurrentUser(User user)
        {
            if (IsCurrentUser(user))
                HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        private bool IsCurrentUser(User user)
        {
            return User.Identity.Name == user.Email;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
