using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDelicious.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            List<Dish> allDishes = dbContext.Dishes.OrderByDescending(c => c.CreatedAt).ToList();
            return View(allDishes);
        }

        [HttpGet("new")]
        public IActionResult NewDish()
        {
            return View();
        }

        [HttpPost("new")]
        public IActionResult createDish(Dish newDish){
            if(ModelState.IsValid){
                dbContext.Add(newDish);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewDish");
        }

        [HttpGet("{dishId}")]
        public IActionResult OneDish(int dishId){
            Dish oneDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId==dishId);
            return View(oneDish);
        }

        [HttpGet("edit/{dishId}")]
        public IActionResult EditDish(int dishId){
            Dish oneDish = dbContext.Dishes.FirstOrDefault(dish => dish.DishId==dishId);
            return View("EditDish", oneDish);
        }

        [HttpPost("edit/{dishId}")]
        public IActionResult editDish(Dish editDish){
            if(ModelState.IsValid){
                Dish dishFromDb = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == editDish.DishId);
                dishFromDb.Name = editDish.Name;
                dishFromDb.Chef = editDish.Chef;
                dishFromDb.Tastiness = editDish.Tastiness;
                dishFromDb.Calories = editDish.Calories;
                dishFromDb.Description = editDish.Description;
                dishFromDb.UpdateddAt = DateTime.Now;dbContext.SaveChanges();
                return RedirectToAction("OneDish",dishFromDb);
            }
            return View("EditDish");
        }

        [HttpGet("deleteDish/{dishId}")]
        public IActionResult deleteDish(int dishId){
                Dish dishFromDb = dbContext.Dishes.FirstOrDefault(dish => dish.DishId == dishId);
                dbContext.Dishes.Remove(dishFromDb);dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
        }


        // public IActionResult Privacy()
        // {
        //     return View();
        // }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
}
