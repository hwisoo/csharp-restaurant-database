using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Models;
using System.Collections.Generic;
using System;

namespace RestaurantApp.Tests
{
  [TestClass]
  public class CuisineTest : IDisposable
  {

    public void Dispose()
    {
      Cuisine.ClearAll();
    }

    public CuisineTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
    }

    [TestMethod]
    public void CuisineConstructor_CreatesInstanceOfCuisine_Cuisine()
    {
      Cuisine newCuisine = new Cuisine("test");
      Assert.AreEqual(typeof(Cuisine), newCuisine.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string Name = "Indian";
      Cuisine newCuisine = new Cuisine(Name, 1);

      //Act
      string result = newCuisine.GetCuisineType();

      //Assert
      Assert.AreEqual(Name, result);
    }

    [TestMethod]
    public void Add_CuisineList_To_Database()
    {
        //Arrange
        string name = "Indian";
        Cuisine newCuisine = new Cuisine(name);
        newCuisine.Save();
        List<Cuisine> newList = new List<Cuisine> {newCuisine};

        //Act
       List<Cuisine> result = Cuisine.GetAll();

       //Assert
       CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Delete_Cuisine_From_Database()
    {
        //Arrange
        string name = "Indian";
        Cuisine newCuisine = new Cuisine(name);
        newCuisine.Save();
        Cuisine.DeleteCuisine(newCuisine.GetId());

        //Act
        List<Cuisine> result = Cuisine.GetAll();
        List<Cuisine> testList = new List<Cuisine>{};

        //Assert
        CollectionAssert.AreEqual(result, testList);
        
    }

    [TestMethod]
    public void Add_Restaurants_To_Cuisine_List()
    {
        //Arrange
         Cuisine newCuisine = new Cuisine("Seafood");
         newCuisine.Save();
         Restaurant testRestaurant = new Restaurant("Bob's", "Seaside seafood restaurant", "Seattle", "Seafood");
         testRestaurant.Save();
         Restaurant testRestaurant2 = new Restaurant("Jim's", "local seafood restaurant", "Seattle", "Indian");
        testRestaurant2.Save();
         //Act
         string cuisineList = newCuisine.GetCuisineType();
         string result = testRestaurant.GetCuisineType();

         //Assert
         Assert.AreEqual(cuisineList,result);
    }

  }
}
