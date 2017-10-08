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
    }
}
