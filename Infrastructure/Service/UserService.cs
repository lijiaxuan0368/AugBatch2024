using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMovieService _movieService;
        private readonly IPurchaseRepository _purchaseRepository;
        public UserService(IUserRepository userRepository, IMovieService movieService, IPurchaseRepository purchaseRepository)
        {
            _userRepository = userRepository;
            _movieService = movieService;   
            _purchaseRepository = purchaseRepository;
        }
        public async Task<UserRegisterResponseModel> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // first check if the email user entered exists in the database
            // if yes, throw an exception or send a message saying email exists
            var user = await _userRepository.GetUserByEmail(requestModel.Email);
            if (user != null)
            {
                // email exist in the database
                throw new Exception($"Email {requestModel.Email} exists, please try again.");
            }

            // continue => Emmail doesn't exist in the DB
            // create a random salt and has the password with the salt
            // Using Library:  Microsoft.AspNetCore.Cryptography.KeyDerivation
            var salt = GenerateSalt();
            var hashedPassword = GenerateHashedPassword(requestModel.Password, salt);

            // create user entity object and call user repo to save
            var newUser = new User
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                DateOfBirth = requestModel.DateOfBirth,
                Salt = salt,
                HashedPassword = hashedPassword
            };

            // Save in DB -> Repository
            var createdUser = await _userRepository.AddAsync(newUser);

            var userRegisterResponseModel = new UserRegisterResponseModel
            {
                Id = createdUser.Id,
                Email = createdUser.Email,
                FirstName = createdUser.FirstName,
                LastName = createdUser.LastName
            };

            return userRegisterResponseModel;
        }

        public async Task<UserLoginResponseModel> ValidateUser(string email, string password)
        {
            // Get the user info from database by email
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                // Don't have the email in the database
                return null;
            }

            // Hash the user entered password along with salt from database
            var hashedPassword = GenerateHashedPassword(password, user.Salt);
            if (hashedPassword == user.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                };
                return userLoginResponseModel;
            }
            return null;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            // See if Movie is already purchased.
            if (await IsMoviePurchased(purchaseRequest, userId))
                throw new Exception("Movie already Purchased");
            // Get Movie Price from Movie Table
            var movie = await _movieService.GetMovieAsync(purchaseRequest.MovieId);

            var purchase = new Purchase
            {
                MovieId = purchaseRequest.MovieId,
                PurchaseNumber = Guid.NewGuid(),
                PurchaseDateTime = DateTime.UtcNow,
                TotalPrice = movie.Price.GetValueOrDefault(),
                UserId = userId
            };
            //  var purchase = _mapper.Map<Purchase>(purchaseRequest);
            var createdPurchase = await _purchaseRepository.AddAsync(purchase);
            return createdPurchase.Id > 0;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            return await _purchaseRepository.GetExistsAsync(p =>
                p.UserId == userId && p.MovieId == purchaseRequest.MovieId);
        }

        // copy from documentation
        private string GenerateSalt()
        {
            byte[] randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

        /* *
         * Hashing Rules:
         * NEVER create your own hashing algorithm -> KeyDerivation.Pbkdf2; Argon2
         * */

        // copy from documentation
        private string GenerateHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }
    }
}
