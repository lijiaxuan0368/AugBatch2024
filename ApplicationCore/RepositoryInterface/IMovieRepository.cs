using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterface
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies();
    }
}
