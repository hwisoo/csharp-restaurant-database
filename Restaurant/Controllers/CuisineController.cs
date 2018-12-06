using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Models;

namespace RestaurantApp.Controllers
{
    public class CuisineController : Controller
    {
      [HttpGet("/cuisines")]
      public ActionResult Index()
      {
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View(allCuisines);
      }

      [HttpPost("/cuisines/new")]
      public ActionResult Create(string cuisinetype)
      {
        Cuisine newCuisine = new Cuisine( cuisinetype);
        newCuisine.Save();
        List<Cuisine> allCuisines = Cuisine.GetAll();  
        return View("Index", allCuisines);
      }

      [HttpGet("/cuisines/new")]
      public ActionResult New()
      { 
        return View();
      }

      [HttpGet("/cuisines/{id}")]
      public ActionResult Show(int id)
      { 
      Dictionary<string, object> model = new Dictionary<string, object>();
      Cuisine selectedCuisine = Cuisine.Find(id);
      List<Restaurant> cuisineRestaurants = selectedCuisine.GetRestaurants();
      model.Add("cuisine", selectedCuisine);
      model.Add("restaurants", cuisineRestaurants);
      return View(model);
      }
        
      [HttpGet("/cuisines/{id}/delete")]
      public ActionResult Delete(int id)
      {
        Cuisine cuisine = Cuisine.Find(id);
        Cuisine.DeleteCuisine(id);
        List<Cuisine> allCuisines = Cuisine.GetAll();
        return View("Index",allCuisines);
    
      }

      


    }
}
