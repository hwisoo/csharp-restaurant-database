using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using RestaurantApp;

namespace RestaurantApp.Models
{
    public class Cuisine
    {
    private string _cuisineType;
    private int _id;

    public Cuisine(string cuisineType, int id = 0)
    {
    
      _cuisineType = cuisineType;
      _id = id;
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
      cmd.CommandText = @"DELETE FROM cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisines = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM Cuisine;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CuisineId = rdr.GetInt32(1);
        string CuisineType = rdr.GetString(0);
        Cuisine newCuisine = new Cuisine( CuisineType, CuisineId);
        allCuisines.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisines;
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine WHERE id = (@searchId);";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CuisineId = 0;
      string CuisineCuisineType = "";
      while(rdr.Read())
      {
        CuisineId = rdr.GetInt32(1);
        CuisineCuisineType = rdr.GetString(0);
      }
      Cuisine newCuisine = new Cuisine( CuisineCuisineType,  CuisineId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCuisine;
    }

    public List<Restaurant> GetRestaurants()
    {
      List<Restaurant> allCuisineRestaurants = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant WHERE cuisineType = @cuisineType;";
      MySqlParameter cuisineType = new MySqlParameter();
      cuisineType.ParameterName = "@cuisineType";
      cuisineType.Value = this._cuisineType;
      cmd.Parameters.Add(cuisineType);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string RestaurantName = rdr.GetString(0);
        string RestaurantDescription = rdr.GetString(1);
        string RestaurantLocation = rdr.GetString(2);
        string RestaurantCuisineType = rdr.GetString(3);
        int RestaurantId = rdr.GetInt32(4);
        Restaurant newRestaurant = new Restaurant(RestaurantName, RestaurantDescription, RestaurantLocation, RestaurantCuisineType,RestaurantId );
        allCuisineRestaurants.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisineRestaurants;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if (!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        bool idEquality = this.GetId().Equals(newCuisine.GetId());
        bool cuisineTypeEquality = this.GetCuisineType().Equals(newCuisine.GetCuisineType());
        return (idEquality && cuisineTypeEquality);
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisine (cuisineType) VALUES (@cuisineType);";
      MySqlParameter cuisineType = new MySqlParameter();
      cuisineType.ParameterName = "@cuisineType";
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

     public static void DeleteCuisine(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine WHERE id = (@thisId);";
      
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
  }
}
