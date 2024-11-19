
using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
        Task<MovieDetailsResponseModel> GetMovieAsync(int id);
    }
}
