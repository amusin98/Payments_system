using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payments_system.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Payments_system.Controllers
{
    //Controller for payments
    [Authorize(Roles = "user")]
    public class PaymentsController : Controller
    {
        private readonly PaymentsContext _context;

        public PaymentsController(PaymentsContext context)
        {
            _context = context;
        }

        //Method for loading form for pay
        [HttpGet]
        public IActionResult Pay()
        {
            ViewBag.Goals = _context.Goals;
            ViewBag.Cards = _context.Cards.Where(card => card.UserId == JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId);
            ViewBag.Accounts = _context.Accounts.Where(acc => acc.Card.UserId == JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId);
            return View();
        }

        //Method for pay
        [HttpPost]
        public IActionResult Pay(int goal, int account, double amount)
        {
            var currentAcc = _context.Accounts.FirstOrDefault(acc => acc.AccountId == account);
            if (!currentAcc.IsBlocked)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    currentAcc.Balance -= amount;
                    if (currentAcc.Balance >= 0)
                    {
                        _context.Cards.FirstOrDefault(card => card.CardId == currentAcc.CardId).Balance -= amount;
                        _context.Payments.Add(new Payment { AccountId = currentAcc.AccountId, Amount = amount, Date = DateTime.Now.ToShortDateString(), GoalId = goal});
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                        ViewBag.Message = "You cant make payment from this account! No resources.";
                        return View("Error");
                    }
                }
            }
            else
            {
                ViewBag.Message = "This account is blocked, operation unavailable!";
                return View("Error");
            }
            return RedirectToAction("Main", "Users");
        }
    }
}
