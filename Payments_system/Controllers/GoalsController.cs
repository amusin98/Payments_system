using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Payments_system.Models;
using Microsoft.AspNetCore.Authorization;

namespace Payments_system.Controllers
{
    [Authorize(Roles = "admin")]
    public class GoalsController : Controller
    {
        private readonly PaymentsContext _context;

        public GoalsController(PaymentsContext context)
        {
            _context = context;
        }

        //Method for adding new payment goal(loading form)
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Goals = _context.Goals;
            return View();
        }

        //Method for adding new payment goal
        [HttpPost]
        public IActionResult Add(string goal)
        {
            if (_context.Goals.Where(x => x.GoalName == goal).Count() == 0)
            {
                _context.Goals.Add(new Goal { GoalName = goal });
                _context.SaveChanges();
                return RedirectToAction("MainAdmin", "Users");
            }
            else
            {
                ViewBag.Message = "This goal already exists!";
                return View("Error");
            }
            
        }

        //Method for updating goal(loading form)
        [HttpGet]
        public IActionResult Update()
        {
            ViewBag.Goals = _context.Goals;
            return View();
        }

        //Method for updating goal
        [HttpPost]
        public IActionResult Update(int goalOld, string goalNew)
        {
            if (_context.Goals.Where(x => x.GoalName == goalNew).Count() == 0)
            {
                _context.Goals.FirstOrDefault(x => x.GoalId == goalOld).GoalName = goalNew;
                _context.SaveChanges();
                return RedirectToAction("MainAdmin", "Users");
            }
            else
            {
                ViewBag.Message = "This goal already exists! Update cancelled!";
                return View("Error");
            }

        }

        //Method for deleting payment goal(loading form)
        [HttpGet]
        public IActionResult Delete()
        {
            ViewBag.Goals = _context.Goals;
            return View();
        }

        //Method for deleting goal
        [HttpPost]
        public IActionResult Delete(int deletingEntity, bool accept=false)
        {
            if (_context.Goals.Include(x => x.Payments).FirstOrDefault(x => x.GoalId == deletingEntity).Payments.Count == 0 || accept)
            {
                _context.Goals.Remove(_context.Goals.FirstOrDefault(x => x.GoalId == deletingEntity));
                _context.SaveChanges();
                return RedirectToAction("MainAdmin", "Users");
            }
            else
            {
                ViewBag.Message = "This goal used for some payments, are you sure that you want delete it? You will some information about pays.";
                ViewBag.Entity = deletingEntity;
                ViewBag.Controller = "Goals";
                return View("Warning");
            }
        }

       
    }
}
