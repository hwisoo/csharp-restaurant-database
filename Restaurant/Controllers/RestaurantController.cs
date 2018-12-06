using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestaurantApp.Models;

namespace RestaurantApp.Controllers
{
    public class RestaurantController : Controller
    {
    [HttpGet("/restaurants")]
      public ActionResult Index()
      {
        List<Restaurant> allRestaurants = Restaurant.GetAll();  
        return View(allRestaurants);
      }

    [HttpGet("/restaurants/new")]
      public ActionResult New()
      {
        return View();
      }

     [HttpPost("/restaurants/new")]
      public ActionResult Create(string name, string description, string location, string cuisinetype)
      {
        Restaurant newRestaurant = new Restaurant(name, description, location, cuisinetype);
        newRestaurant.Save();
        List<Restaurant> allRestaurants = Restaurant.GetAll();  
        return View("Index", allRestaurants);
      }

     [HttpGet("/restaurants/{id}")]
      public ActionResult Show(int id)
      {
        Restaurant currentRestaurant = Restaurant.Find(id);  
       
        return View(currentRestaurant);
      }

      [HttpGet("/restaurants/{id}/delete")]
      public ActionResult Delete(int id)
      {
        // Restaurant restaurant = Restaurant.Find(id);
      // Restaurant.DeleteRestaurant(restaurant.GetId());
        Restaurant.DeleteRestaurant(id);
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View("Index", allRestaurants);
      }

      [HttpPost("/restaurants/{id}/edit")]
      public ActionResult Update(string name,string description, string location, string cuisinetype , int id)
      {
        Restaurant restaurant = Restaurant.Find(id);
        restaurant.Edit(name, description, location, cuisinetype);
        List<Restaurant> allRestaurants = Restaurant.GetAll();
        return View("Index", allRestaurants);
      }

      [HttpGet("restaurants/{id}/edit")]
      public ActionResult Edit(int id)
      {
        Restaurant currentRestaurant = Restaurant.Find(id);  
        return View(currentRestaurant);
      }
    }
}
