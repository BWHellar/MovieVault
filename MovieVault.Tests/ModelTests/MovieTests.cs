using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MovieVault.Models;

namespace MovieVault.Tests
{
  [TestClass]
  public class MovieTest : IDisposable
  {
    public void Dispose()
    {
      Movie.ClearAll();
    }

    public MovieTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=movie_vault_test;";
    }

    [TestMethod]
    public void MovieConstructor_CreatesInstanceOfMovie_Movie()
    {
      Movie newMovie = new Movie("The Matrix", "The Wachowski Siblings", 1999);
      Assert.AreEqual(typeof(Movie), newMovie.GetType());
    }

  }
}
