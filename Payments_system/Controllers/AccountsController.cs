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
    //Controller for account entity
    public class AccountsController : Controller
    {
        private readonly PaymentsContext _context;

        public AccountsController(PaymentsContext context)
        {
            _context = context;
        }

        //Method for getting form for create account
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Create()
        {
            var cards =  _context.Cards.Include(x => x.Account).Include(x => x.User).Where(card => card.User.Email == User.Identity.Name);
            ViewBag.FreeCards = _context.Cards.Include(x => x.Account).Include(x => x.User).Where(card => card.User.Email == User.Identity.Name && card.Account == null);
            return View(cards);
        }

        //Method for creating account
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Create(int card)
        {
            _context.Accounts.Add(new Account { CardId = card, IsBlocked = false, Balance = _context.Cards.FirstOrDefault(c => c.CardId == card).Balance});
            _context.SaveChanges();
            return RedirectToAction("Main", "Users");
        }

        //Method for loading form for blocking account
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Block()
        {
            var accounts = _context.Accounts.Include(x => x.Card.User).Where(acc => acc.Card.User.Email == User.Identity.Name);
            ViewBag.UnBlockedAccounts = _context.Accounts.Include(x => x.Card.User).Where(acc => acc.Card.User.Email == User.Identity.Name && acc.IsBlocked == false);
            return View(accounts);
        }

        //Method for blocking account
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Block(int account)
        {
            _context.Accounts.FirstOrDefault(acc => acc.AccountId == account).IsBlocked = true;
            _context.SaveChanges();
            return RedirectToAction("Main", "Users");
        }

        //Method for loading form for replenish account
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Replenish()
        {
            ViewBag.Accounts = _context.Accounts.Include(x => x.Card.User).Where(acc => acc.Card.User.Email == User.Identity.Name);
            ViewBag.Cards = _context.Cards.Include(x => x.User).Where(card => card.User.Email == User.Identity.Name);
            return View();
        }

        //Method for replenish account
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Replenish(int account, int card, double amount)
        {
            if (amount > 0)
            {
                if (!(_context.Accounts.FirstOrDefault(acc => acc.AccountId == account).CardId == card))
                {
                    var CurrentCard = _context.Cards.FirstOrDefault(c => c.CardId == card);
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        CurrentCard.Balance -= amount;
                        if (CurrentCard.Balance >= 0)
                        {
                            _context.Accounts.FirstOrDefault(acc => acc.AccountId == account).Balance += amount;
                            if (!(_context.Accounts.Where(acc => acc.CardId == card).Count() == 0))
                            {
                                _context.Accounts.FirstOrDefault(acc => acc.CardId == card).Balance -= amount;
                            }

                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        else
                        {
                            transaction.Rollback();
                            ViewBag.Message = "You haven't resources for this operation!";
                            return View("Error");
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "You cant replenish account from this card! It is card binded to this account.";
                    return View("Error");
                }
                return RedirectToAction("Main", "Users");
            }
            else
            {
                ViewBag.Message = "Incorrect amount for replenish!";
                return View("Error");
            }
            
        }

        //Method for unblocking account(for admin)
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Unblock(int account)
        {
            _context.Accounts.FirstOrDefault(acc => acc.AccountId == account).IsBlocked = false;
            _context.SaveChanges();
            ViewBag.Accounts = _context.Accounts.Include(x => x.Card.User);
            ViewBag.BlockedAccs = _context.Accounts.Where(acc => acc.IsBlocked);
            return RedirectToAction("MainAdmin", "Users");
        }

        //Method for deleting account(loading form)
        [HttpGet]
        [Authorize(Roles = "user")]
        public IActionResult Delete()
        {
            ViewBag.Accounts = _context.Accounts.Include(x => x.Card.User).Where(x => x.Card.User.Email == User.Identity.Name);
            return View();
        }

        //Method for deleting account
        [HttpPost]
        [Authorize(Roles = "user")]
        public IActionResult Delete(int deletingEntity, bool accept = false)
        {
            if (_context.Accounts.Include(x => x.Payments).FirstOrDefault(x => x.AccountId == deletingEntity).Payments.Count == 0 || accept)
            {
                _context.Accounts.Remove(_context.Accounts.FirstOrDefault(x => x.AccountId == deletingEntity));
                _context.SaveChanges();
                return RedirectToAction("Main", "Users");
            }
            else
            {
                WarningViewModel wvm = new WarningViewModel { Controller = "Accounts", Entity = deletingEntity, Message = "Are you sure that you want delete this account? Information about payments linked with this account will be lost!" };
                return View("Warning", wvm);
            }
        }
    }
}
