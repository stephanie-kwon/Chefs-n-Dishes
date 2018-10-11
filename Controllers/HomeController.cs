using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Chefs.Models;
using Microsoft.EntityFrameworkCore;

namespace Chefs.Controllers
{
    public class HomeController : Controller
    {
        private YourContext dbContext;
        public HomeController(YourContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            List<User> Allchefs = dbContext.Chef.Include(user => user.dishes).ToList();
            ViewBag.Chefs = Allchefs;
            return View();
        }

        [HttpGet]
        [Route("Dishes")]
        public IActionResult Dishes()
        {            
            List<Dish> AllDish = dbContext.Dish.Include(dish => dish.Chef).ToList();
            ViewBag.Dish = AllDish;
            return View();
        }

        [HttpGet]
        [Route("NewDish")]
        public IActionResult NewDish()
        {
            List<User> AllChefs = dbContext.Chef.Include(user => user.dishes).ToList();
            ViewBag.Chefs = AllChefs;
            return View();
        }
        [HttpPost]
        [Route("NewDish")]
        public IActionResult Newdish(Dish d)
        {
            if(ModelState.IsValid)
            {
        
                dbContext.Add(d);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                List<User> Allchefs = dbContext.Chef.ToList();
                ViewBag.Chefs = Allchefs;
                return View("NewDish");
            }
        }

        [HttpGet]
        [Route("NewChef")]
        public IActionResult NewChef()
        {
            return View();
        }

        [HttpPost]
        [Route("NewChef")]
        public IActionResult NewChef(User user)
        {
            if(ModelState.IsValid)
            {
                if(user.DOB >= DateTime.Today)
                {
                    ModelState.AddModelError("DOB", "DOB must be a date from the past");
                    return View("NewChef");
                }

                User newChef = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DOB = user.DOB
                    
                };
                dbContext.Add(newChef);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("NewChef");
            }
        }
    }
}
