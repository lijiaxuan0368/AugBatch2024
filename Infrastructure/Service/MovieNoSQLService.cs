using ApplicationCore.Entities;
using ApplicationCore.ServiceInterface;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.Models;

namespace Infrastructure.Service
{
    public class MovieNoSQLService : IMovieService
    {
        // E.g. Repo gets data from NoSQL DB
        private readonly IMovieRepository _movieRepository;
        public MovieNoSQLService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IEnumerable<MovieCardResponseModel> GetTopRatedMovies()
        {
            var movies = _movieRepository.GetTopRatedMovies();
            var moviesCardResponseModel = new List<MovieCardResponseModel>();

            foreach (var movie in movies)
            {
                moviesCardResponseModel.Add(new MovieCardResponseModel
                {
                    Id = movie.Id + 100,
                    PosterUrl = movie.PosterUrl,
                });
            }

            return moviesCardResponseModel;
        }
    }
}
