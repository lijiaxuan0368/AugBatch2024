using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterface
{
    public interface IUserRepository : IAsyncRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
