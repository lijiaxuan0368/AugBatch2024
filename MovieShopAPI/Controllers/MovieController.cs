using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        // api/movie/gettopratedmovies
        [Route("TopRated")]
        // api/movie/toprated
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTopRatedMovies();
            if (!movies.Any())
            {
                return NotFound("No movies Found"); // 404
            }

            return Ok(movies); //200

            // Serialization => object to another type of Object
            // C# => JSON
            // Deserialization => JSON -> C#
            // System.Text.Json
            // old 3rd party library, JSON.NET

            if (!User.Identity.IsAuthenticated) { 
             return Unauthorized(); //401
                // BadRequest() // 400
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            return Ok();
        }
    }
}
