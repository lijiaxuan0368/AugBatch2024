using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterface
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetTopRatedMovies();
    }
}
