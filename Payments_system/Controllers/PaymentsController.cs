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
using Payments_system.ViewModels;

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
            MakePayViewModel mpvm = new MakePayViewModel { Goals = _context.Goals, Cards = _context.Cards.Include(x => x.User).Where(card => card.User.Email == User.Identity.Name), Accounts = _context.Accounts.Include(x => x.Card.User).Where(acc => acc.Card.User.Email == User.Identity.Name) };
            return View(mpvm);
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
