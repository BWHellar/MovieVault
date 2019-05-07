using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MovieVault.Models
{
  public class MovieVault
  {
    private string _name;
    private string _director;
    private int _year;
    private int _id;

    public movie (string name, string director, int year, int id = 0)
    {
      _name = name;
      _director = director;
      _year = year;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public string GetDirector()
    {
      return _director;
    }

    public int GetYear()
    {
      return _year;
    }

    public int (GetId)
    {
      return _id;
    }


    public override bool Equals(System.Object otherMovie)
    {
      if (!(otherMovie is Movie))
      {
        return false;
      }
      else
      {
        Movie newMovie = (Movie) otherMovie;
        bool idEquality = (this.GetId() == newMovie.GetId());
        bool descriptionEquality = (this.GetDescription() == newMovie.GetDescription());
        return (idEquality && descriptionEquality);
      }
    }

    public static Movie Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Opn();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM 'movies' WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int movieId = 0;
      string movieName = "";
      string movieDirector = "";
      int movieYear = 0;
      while (rdr.Read())
      {
        movieId = rdr.GetInt32(0);
        movieName = rdr.GetString(1);
        movieDirector = rdr.GetString(2);
        movieYear = rdr.GetInt32(3);
      }
      Movie foundMovie = new Movie(movieName, movieDirector, movieYear, movieId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundMovie;
    }

    public static List<Movie> GetAll()
    {
      List<Movie> allMovies = new List<Movie> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM movies;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        movieId = rdr.GetInt32(0);
        movieName = rdr.GetString(1);
        movieDirector = rdr.GetString(2);
        movieYear = rdr.GetInt32(3);
        Movie newMovie = new Movie(movieName, movieDirector, movieYear, movieId);
        allMovies.Add(newMovie);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allMovies;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO movies (name, director, year) VALUES (@MovieName, @MovieDirector, @MovieYear);";
      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@MovieName";
      name.Value = _name;
      MySqlParameter director = new MySqlParameter();
      director.ParameterName = "@MovieDirector";
      director.Value = _director;
      MySqlParameter year = new MySqlParameter();
      year.ParameterName = "@MovieYear";
      year.Value = _year;
      cmd.Parameters.Add(name, director, year);
      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM movies";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn !=null)
      {
        conn.Dispose();
      }
    }
  }
}
