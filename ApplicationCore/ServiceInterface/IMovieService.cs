
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
    }
}
