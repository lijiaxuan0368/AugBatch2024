
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IMovieService
    {
        IEnumerable<MovieCardResponseModel> GetTopRatedMovies();
    }
}
