using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;

namespace Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> GetTopRatedMovies()
        {
            // DB retrieve data
            var movies = new List<Movie> {
                new Movie{Id = 1,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 2,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 3,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 4,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764},
                new Movie{Id = 5,Title = "Inception",PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg",Revenue = 825532764}
            };

            return movies;
        }
    }
}
