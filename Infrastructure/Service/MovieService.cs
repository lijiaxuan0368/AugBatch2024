using ApplicationCore.Entities;
using ApplicationCore.ServiceInterface;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.Models;

namespace Infrastructure.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<MovieCardResponseModel> GetTopRatedMovies()
        {
            // MAPPING => Movie -> MovieCardRepsonseModel
            //var movies = _movieRepository.GetTopRatedMovies();
            var movies = new List<Movie> {
                new Movie{Id = 1,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 2,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 3,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 4,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 5,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764}
            };

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                });
            }

            return movieCardList;
        }
    }
}
