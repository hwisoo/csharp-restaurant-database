using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestaurantApp.Models;
using System.Collections.Generic;
using System;

namespace RestaurantApp.Tests
{
  [TestClass]
  public class RestaurantTest : IDisposable
  {

    public RestaurantTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=restaurant_test;";
    }

    public void Dispose()
    {
      Cuisine.ClearAll();
      Restaurant.ClearAll();
    }

    [TestMethod]
    public void RestaurantConstructor_CreatesInstanceOfRestaurant_Restaurant()
    {
      Restaurant newRestaurant = new Restaurant("Big Fish", "Seaside seafood restaurant", "Seattle", "Seafood");
      Assert.AreEqual(typeof(Restaurant), newRestaurant.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "Test Category";
      string description = "good";
      string location = "seattle";
      string cuisineType = "food";
      Restaurant newRestaurant = new Restaurant(name , description, location , cuisineType);

      //Act
      string result = newRestaurant.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetId_ReturnsRestaurantId_Int()
    {
      //Arrange
      string name = "Test Category";
      string description = "good";
      string location = "seattle";
      string cuisineType = "food";
      Restaurant newRestaurant = new Restaurant(name, description, location , cuisineType);
    
      //Act
      newRestaurant.Save();
      int result = newRestaurant.GetId();
    
        Console.WriteLine(newRestaurant.GetId());
      //Assert
      Assert.AreEqual(newRestaurant.GetId(), result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllResataurantObjects_ResataurantList()
    {
      //Arrange
      string name = "Work";
      string description = "good";
      string location = "seattle";
      string cuisineType = "food";
      string name2 = "School";
      string description2 = "good";
      string location2 = "seattle";
      string cuisineType2 = "food";
      Restaurant newRestaurant1 = new Restaurant(name, description, location , cuisineType);
      newRestaurant1.Save();
      Restaurant newRestaurant2 = new Restaurant(name2, description2, location2 , cuisineType2);
      newRestaurant2.Save();
      List<Restaurant> newList = new List<Restaurant> { newRestaurant1, newRestaurant2 };

      //Act
      List<Restaurant> result = Restaurant.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsRestaurantInDatabase_Restaurant()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Big Fish", "Seaside seafood restaurant", "Seattle", "Seafood");
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.AreEqual(testRestaurant, foundRestaurant);
    }

    [TestMethod]
    public void GetRestaurant_ReturnsEmptyList_AfterDelete()
    {
      //Arrange
      string name = "Test Category";
      string description = "good";
      string location = "seattle";
      string cuisineType = "food";
      Restaurant newRestaurant = new Restaurant(name, description , location, cuisineType);
      newRestaurant.Save();
      Restaurant.DeleteRestaurant(newRestaurant.GetId());

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> newList = new List<Restaurant> {};

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetRestaurant_ReturnsUpdatedList_AfterEdit()
    {
      //Arrange
      string name = "Test Category";
      string description = "good";
      string location = "seattle";
      string cuisineType = "food";
      string name2 = "School";
      string description2 = "good";
      string location2 = "seattle";
      string cuisineType2 = "food";
      Restaurant newRestaurant = new Restaurant(name, description , location, cuisineType);
      newRestaurant.Save();

      newRestaurant.Edit(name2, description2, location2 , cuisineType2);

      //Act
      string result = newRestaurant.GetName();

      //Assert
     Assert.AreEqual(name2, result);
    }


    [TestMethod]
    public void Save_SavesRestaurantToDatabase_RestaurantList()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Household chores", "Seaside seafood restaurant", "Seattle", "Seafood");
      testRestaurant.Save();

      //Act
      List<Restaurant> result = Restaurant.GetAll();
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToRestaurant_Id()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("Household chores","Seaside seafood restaurant", "Seattle", "Seafood");
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }


  }
}
