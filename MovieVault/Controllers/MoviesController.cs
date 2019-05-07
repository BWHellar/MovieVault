using Microsoft.AspNetCore.Mvc;
using MovieVault.Models;
using System.Collections.Generic;

namespace MovieVault.Controllers
{
  public class MoviesController : Controller
  {

    [HttpGet("genres/{genreId}/movies/new")]
    public ActionResult New(int genreId)
    {
      Genre genre = Genre.Find(genreId);
      return View(genre);
    }

    [HttpGet("/genre/{genreId}/movies/{movieId}")]
    public ActionResult Show(int genreId, int movieId)
    {
      Movie movie = Movie.Find(movieId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Genre genre = Genre.Find(genreId);
      model.Add("movie", movie);
      model.Add("genre", genre);
      return View(model);
    }

    [HttpPost("/movie/delete")]
    public ActionResult DeleteAll()
    {
      Movie.ClearAll();
      return View();
    }
  }
}
