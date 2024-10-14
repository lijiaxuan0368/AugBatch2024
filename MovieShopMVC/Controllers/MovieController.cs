using ApplicationCore.ServiceInterface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    {
        // Dependency Injection
        // Create Loosely-coupled code

        // Instance of Class
        private readonly IMovieService _movieService;

        // Constructor Injection
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // www.walmart.com/movie/TopRated

        [HttpGet]
        public IActionResult TopRated()
        {
            var movies = _movieService.GetTopRatedMovies();

            return View(movies);
        }
        // UserController
        // GenreController


        // www.walmart.com/movie/Details/id
        public IActionResult Details(int id)
        {
            var movies = _movieService.GetTopRatedMovies();
            var movie = movies.Select(m => m.Id = id).FirstOrDefault();

            return View(movie);
        }
    }
}
