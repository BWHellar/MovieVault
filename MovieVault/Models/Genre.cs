using System.Collections.Generic;

namespace MovieVault.Models
{
  public class Genre
  {
    private static List<Genre> _instances = new List<Genre> {};
    private string _name;
    private int _id;
    private List<Movie> _movies;

    public Genre(string genreName)
    {
      _name = genreName;
      _instances.Add(this);
      _id = _instances.Count;
      _movies = new List<Movie>{};
    }

    public string GetName()
    {
      return _name;
    }
    public int GetId()
    {
      return _id;
    }
    public static void ClearAll()
    {
      _instances.Clear();
    }
    public static List<Genre> GetAll()
    {
      return _instances;
    }
    public static Genre Find(int searchId)
    {
      return _instances[searchId-1];
    }
    public List<Movie> GetMovie()
    {
      return _movies;
    }
    public void AddMovie(Movie movie)
    {
      _movies.Add(movie);
    }
  }
}
