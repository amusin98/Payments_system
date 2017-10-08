using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Payments_system.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Payments_system.Controllers
{
    public class UsersController : Controller
    {
        private readonly PaymentsContext _context;

        public UsersController(PaymentsContext context)
        {
            _context = context;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // Get method for registration
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        //Post method for registration
        [HttpPost]
        public IActionResult Registration(string name, string surname, string email, string password)
        {
            if (_context.Users.Where(u => u.Email.Equals(email)).Count() == 0)
            {
                _context.Users.Add(new User { Name = name, Surname = surname, Email = email, IsAdmin = false, Password = password });
                _context.SaveChanges();
                return View("Login");
            } else
            {
                ViewBag.Message = "User with this email exists, use other email or login with correct data!";
                return View("Error");
            }
        }

        //Get method for login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //Method for user authentificate
        private void Authentificate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.IsAdmin ? "admin" : "user")
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        //Post method for login
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            User user = _context.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            if (user != null)
            {
                Authentificate(user);
                HttpContext.Session.SetString("user", JsonConvert.SerializeObject(user));
                if (user.IsAdmin)
                {
                    ViewBag.Accounts = _context.Accounts.Include(x => x.Card.User);
                    ViewBag.BlockedAccs = _context.Accounts.Where(acc => acc.IsBlocked);
                    return View("MainAdmin");
                }
                else
                {
                    return View("Main", _context.Payments.Include(x => x.Goal).Include(x => x.Account.Card.User).Where(pay => pay.Account.Card.UserId == JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId));
                }
            }
            else
            {
                ViewBag.Message = "No thats user. Try again or choose registration!";
                return View("Error");
            }
        }

        //Get method for login out
        [Authorize(Roles = "admin, user")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }

        
        //Get method for loading main page of user(used as aim of link on main page for return to main page)
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Main()
        {
            return View(_context.Payments.Include(x => x.Goal).Include(x => x.Account.Card.User).Where(pay => pay.Account.Card.UserId == JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId));
        }
    }
}
