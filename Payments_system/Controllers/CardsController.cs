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
    //Access to this controller only for users
    [Authorize(Roles = "user")]
    public class CardsController : Controller
    {
        private readonly PaymentsContext _context;

        public CardsController(PaymentsContext context)
        {
            _context = context;
        }

        //Method for creating card(loading form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Method for creating card
        [HttpPost]
        public IActionResult Create(double balance)
        {
            _context.Cards.Add(new Card { Balance = balance, UserId = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId });
            _context.SaveChanges();
            return RedirectToAction("Main", "Users");
        }

        //Method for deleting card(loading form)
        [HttpGet]
        public IActionResult Delete()
        {
            ViewBag.Cards = _context.Cards.Where(x => x.UserId == JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user")).UserId);
            return View();
        }

        //Method for deleting card
        [HttpPost]
        public IActionResult Delete(int deletingEntity, bool accept = false)
        {
            if (_context.Cards.Include(x => x.Account).FirstOrDefault(x => x.CardId == deletingEntity).Account == null || accept)
            {
                _context.Cards.Remove(_context.Cards.FirstOrDefault(x => x.CardId == deletingEntity));
                _context.SaveChanges();
                return RedirectToAction("Main", "Users");
            }
            else
            {
                ViewBag.Controller = "Cards";
                ViewBag.Entity = deletingEntity;
                ViewBag.Message = "Are you sure that you want delete this card? Information about accounts and payments linked with this card will be lost!";
                return View("Warning");
            }
        }
    }
}
