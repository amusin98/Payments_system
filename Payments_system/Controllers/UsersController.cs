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
using Payments_system.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Payments_system.Controllers
{
    public class UsersController : Controller
    {
        private readonly PaymentsContext _context;

        public UsersController(PaymentsContext context)
        {
            StartData.Initialize(context);
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
                if (user.IsAdmin)
                {
                    return RedirectToAction("MainAdmin","Users");
                }
                else
                {
                    return RedirectToAction("Main", "Users");
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
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }

        
        //Get method for loading main page of user(used as aim of link on main page for return to main page)
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Main(int? payment, string goal, int? account, string date, int page = 1, PaymentsSortState sortOrder = PaymentsSortState.PaymentIdAsc)
        {
            int pageSize = 5;
            IQueryable<Payment> source = _context.Payments.Include(x => x.Goal).Include(x => x.Account.Card.User).Where(pay => pay.Account.Card.User.Email == User.Identity.Name);

            if (payment != null && payment != 0)
            {
                source = source.Where(x => x.PaymentId == payment);
            }
            if (account != null && account != 0)
            {
                source = source.Where(x => x.AccountId == account);
            }
            if (goal != null)
            {
                source = source.Where(x => x.Goal.GoalName.Contains(goal));
            }
            if (date != null)
            {
                source = source.Where(x => x.Date.Contains(date));
            }

            switch (sortOrder)
            {
                case PaymentsSortState.PaymentIdAsc:
                    source = source.OrderBy(x => x.PaymentId);
                    break;
                case PaymentsSortState.PaymentIdDesc:
                    source = source.OrderByDescending(x => x.PaymentId);
                    break;
                case PaymentsSortState.AccIdAsc:
                    source = source.OrderBy(x => x.AccountId);
                    break;
                case PaymentsSortState.AccIdDesc:
                    source = source.OrderByDescending(x => x.AccountId);
                    break;
                case PaymentsSortState.GoalAsc:
                    source = source.OrderBy(x => x.Goal.GoalName);
                    break;
                case PaymentsSortState.GoalDesc:
                    source = source.OrderByDescending(x => x.Goal.GoalName);
                    break;
                case PaymentsSortState.AmountAsc:
                    source = source.OrderBy(x => x.Amount);
                    break;
                case PaymentsSortState.AmountDesc:
                    source = source.OrderByDescending(x => x.Amount);
                    break;
                case PaymentsSortState.DateAsc:
                    source = source.OrderBy(x => DateTime.Parse(x.Date).Date);
                    break;
                case PaymentsSortState.DateDesc:
                    source = source.OrderByDescending(x => DateTime.Parse(x.Date).Date);
                    break;
                default:
                    source = source.OrderBy(x => x.PaymentId);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            UserPaymentsViewModel ivm = new UserPaymentsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortPaymentsViewModel(sortOrder),
                FilterViewModel = new FilterPaymentsViewModel(goal, date, account, payment),
                Payments = items
            };
            ViewBag.User = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name).Name;
            return View("Main", ivm);
        }


        //Get method for loading main page of user(used as aim of link on main page for return to main page)
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult MainAdmin(int? card, string email, int? account, int page=1, AccountsSortState sortOrder = AccountsSortState.AccIdAsc)
        {
            int pageSize = 10;
            IQueryable<Account> source = _context.Accounts.Include(x => x.Card.User);

            if (card != null && card != 0)
            {
                source = source.Where(x => x.CardId == card);
            }
            if (account != null && account != 0)
            {
                source = source.Where(x => x.AccountId == account);
            }
            if (email != null)
            {
                source = source.Where(x => x.Card.User.Email.Contains(email));
            }

            switch (sortOrder)
            {
                case AccountsSortState.AccIdAsc:
                    source = source.OrderBy(x => x.AccountId);
                    break;
                case AccountsSortState.AccIdDesc:
                    source = source.OrderByDescending(x => x.AccountId);
                    break;
                case AccountsSortState.CardIdAsc:
                    source = source.OrderBy(x => x.CardId);
                    break;
                case AccountsSortState.CardIdDesc:
                    source = source.OrderByDescending(x => x.CardId);
                    break;
                case AccountsSortState.EmailAsc:
                    source = source.OrderBy(x => x.Card.User.Email);
                    break;
                case AccountsSortState.EmailDesc:
                    source = source.OrderByDescending(x => x.Card.User.Email);
                    break;
                case AccountsSortState.BalanceAsc:
                    source = source.OrderBy(x => x.Balance);
                    break;
                case AccountsSortState.BalanceDesc:
                    source = source.OrderByDescending(x => x.Balance);
                    break;
                case AccountsSortState.StatusAsc:
                    source = source.OrderBy(x => x.IsBlocked);
                    break;
                case AccountsSortState.StatusDesc:
                    source = source.OrderByDescending(x => x.IsBlocked);
                    break;
                default:
                    source = source.OrderBy(x => x.AccountId);
                    break;
            }



            var count = source.Count();
            var items = source.Skip((page - 1) * pageSize).Take(pageSize);
            PageViewModel pageView = new PageViewModel(count, page, pageSize);
            AdminAccountsViewModel ivm = new AdminAccountsViewModel
            {
                PageViewModel = pageView,
                SortViewModel = new SortAccountsViewModel(sortOrder),
                FilterViewModel = new FilterAccountsViewModel(card, email, account),
                Accounts = items
            };
            ViewBag.BlockedAccs = _context.Accounts.Where(acc => acc.IsBlocked);
            ViewBag.User = _context.Users.FirstOrDefault(x => x.Email == User.Identity.Name).Name;
            return View("MainAdmin", ivm);
        }
    }
}
