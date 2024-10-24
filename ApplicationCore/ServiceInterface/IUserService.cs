using ApplicationCore.Models;

namespace ApplicationCore.ServiceInterface
{
    public interface IUserService
    {
        Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> ValidateUser(string email, string password);
    }

}
