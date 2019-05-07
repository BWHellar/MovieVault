using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace MovieVault.Models
{
  public class Movie
  {
    private string _title;
    private string _director;
    private int _year;
    private int _id;

    public Movie (string title, string director, int year, int id = 0)
    {
      _title = title;
      _director = director;
      _year = year;
      _id = id;
    }

    public string GetTitle()
    {
      return _title;
    }

    public string GetDirector()
    {
      return _director;
    }

    public int GetYear()
    {
      return _year;
    }

    public int GetId()
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
        bool titleEquality = (this.GetTitle() == newMovie.GetTitle());
        bool directorEquality = (this.GetDirector() == newMovie.GetDirector());
        bool yearEquality = (this.GetYear() == newMovie.GetYear());
        return (idEquality && titleEquality && directorEquality && yearEquality);
      }
    }

    public static Movie Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM 'movies' WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int movieId = 0;
      string movieTitle = "";
      string movieDirector = "";
      int movieYear = 0;
      while (rdr.Read())
      {
        movieId = rdr.GetInt32(0);
        movieTitle = rdr.GetString(1);
        movieDirector = rdr.GetString(2);
        movieYear = rdr.GetInt32(3);
      }
      Movie foundMovie = new Movie(movieTitle, movieDirector, movieYear, movieId);

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
        int movieId = rdr.GetInt32(0);
        string movieTitle = rdr.GetString(1);
        string movieDirector = rdr.GetString(2);
        int movieYear = rdr.GetInt32(3);
        Movie newMovie = new Movie(movieTitle, movieDirector, movieYear, movieId);
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
      cmd.CommandText = @"INSERT INTO movies (title, director, year) VALUES (@Movietitle, @MovieDirector, @MovieYear);";
      MySqlParameter title = new MySqlParameter();
      title.ParameterName = "@Movietitle";
      title.Value = _title;
      cmd.Parameters.Add(title);
      MySqlParameter director = new MySqlParameter();
      director.ParameterName = "@MovieDirector";
      director.Value = _director;
      cmd.Parameters.Add(director);
      MySqlParameter year = new MySqlParameter();
      year.ParameterName = "@MovieYear";
      year.Value = _year;
      cmd.Parameters.Add(year);
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
