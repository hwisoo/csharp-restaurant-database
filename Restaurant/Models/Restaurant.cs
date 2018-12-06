using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestaurantApp;

namespace RestaurantApp.Models
{
    public class Restaurant
    {
    private string _name;
    private string _description;
    private string _location;
    private string _cuisineType;
    private int _id;

    public Restaurant(string name,string description, string location, string cuisineType, int id = 0)
    {
      _name = name;
      _description = description;
      _location = location;
      _cuisineType = cuisineType;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

        public string GetDescription()
    {
      return _description;
    }

        public string GetLocation()
    {
      return _location;
    }
        public string GetCuisineType()
    {
      return _cuisineType;
    }

    public int GetId()
    {
      return _id;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurant;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int RestaurantId = rdr.GetInt32(4);
        string RestaurantName = rdr.GetString(0);
        string RestaurantDescription = rdr.GetString(1);
        string RestaurantLocation = rdr.GetString(2);
        string CuisineType = rdr.GetString(3);
        Restaurant newRestaurant = new Restaurant(RestaurantName,RestaurantDescription, RestaurantLocation, CuisineType, RestaurantId);
        allRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurants;
    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int RestaurantId = 0;
      string RestaurantName = "";
      string RestaurantDescription = "";
      string RestaurantLocation = "";
      string RestaurantCuisineType = "";
      while(rdr.Read())
      {
        RestaurantId = rdr.GetInt32(4);
        RestaurantName = rdr.GetString(0);
        RestaurantDescription = rdr.GetString(1);
        RestaurantLocation = rdr.GetString(2);
        RestaurantCuisineType = rdr.GetString(3);
      }
      Restaurant newRestaurant = new Restaurant(RestaurantName, RestaurantDescription, RestaurantLocation, RestaurantCuisineType,  RestaurantId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId().Equals(newRestaurant.GetId());
        bool nameEquality = this.GetName().Equals(newRestaurant.GetName());
        bool descriptionEquality = this.GetDescription().Equals(newRestaurant.GetDescription());
        bool locationEquality = this.GetLocation().Equals(newRestaurant.GetLocation());
        bool cuisineTypeEquality = this.GetCuisineType().Equals(newRestaurant.GetCuisineType());
        return (idEquality && nameEquality && descriptionEquality && locationEquality && cuisineTypeEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurant (name , description , location, cuisineType) VALUES (@name , @description, @location,@cuisineType);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this._description;
      cmd.Parameters.Add(description);

      MySqlParameter location = new MySqlParameter();
      location.ParameterName = "@location";
      location.Value = this._location;
      cmd.Parameters.Add(location);

      MySqlParameter cuisineType = new MySqlParameter();
      cuisineType.ParameterName= "@cuisineType";
      cuisineType.Value = this._cuisineType;
      cmd.Parameters.Add(cuisineType);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

     public static void DeleteRestaurant(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurant WHERE id = (@thisId);";
      
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
       conn.Dispose();
      }
    }

    public void Edit(string newName, string newDescription, string newLocation, string newCuisineType)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE restaurant SET name = @newName, description = @newDescription, location = @newLocation, cuisineType = @newCuisineType WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter restaurantName = new MySqlParameter();
      restaurantName.ParameterName = "@newName";
      restaurantName.Value = newName;
      cmd.Parameters.Add(restaurantName);

      MySqlParameter RestaurantDescription = new MySqlParameter();
      RestaurantDescription.ParameterName = "@newDescription";
      RestaurantDescription.Value = newDescription;
      cmd.Parameters.Add(RestaurantDescription);

      MySqlParameter RestaurantLocation = new MySqlParameter();
      RestaurantLocation.ParameterName = "@newLocation";
      RestaurantLocation.Value = newLocation;
      cmd.Parameters.Add(RestaurantLocation);

      MySqlParameter RestaurantCuisineType = new MySqlParameter();
      RestaurantCuisineType.ParameterName = "@newCuisineType";
      RestaurantCuisineType.Value = newCuisineType;
      cmd.Parameters.Add(RestaurantCuisineType);

      cmd.ExecuteNonQuery();
      _name = newName;
      _description = newDescription; 
      _location = newLocation;
      _cuisineType = newCuisineType;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
