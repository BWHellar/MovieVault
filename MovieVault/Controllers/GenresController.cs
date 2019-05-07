using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using MovieVault.Models;

namespace MovieVault.Controllers
{
  public class GenresController : Controller
  {

    [HttpGet("/genres")]
    public ActionResult Index()
    {
      List<Genre> allGenres = Genre.GetAll();
      return View(allGenres);
    }

    [HttpGet("/genres/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/genres")]
    public ActionResult Create(string genreName)
    {
      Genre newGenre = new Genre(genreName);
      List<Genre> allGenres = Genre.GetAll();
      return View("Index", allGenres);
    }

    [HttpGet("/genre/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Genre selectedGenre = Genre.Find(id);
      List<Movie> genreMovie = selectedGenre.GetItem();
      model.Add("genre", selectedGenre);
      model.Add("movies", genreMovie);
      return View(model);
    }

    [HttpGet("/genres/{genreId}/movies")]
    public ActionResult Create(int genreId. string movieDescription)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Genre foundGenre = Genre.Find(GenreId);
      Movies newMovie = new Movie(movieDescription);
      newMovie.Save();
      foundGenre.AddItem(newMovie);
      List<Movie> genreMovie = foundGenre.GetMovie();
      model.Add("movie", genreMovie);
      model.Add("genre", foundGenre);
      return View("Show", model);
    }
  }
}
