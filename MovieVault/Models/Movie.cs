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
    

  }
}
