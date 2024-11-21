using ApplicationCore.ServiceInterface;
using Infrastructure.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.Models;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        // Dependency Injection -> UserService
        private readonly IMovieService _movieService;
        private readonly IUserService _userService;
        private ICurrentUserService _currentUserService;

        public UserController(IUserService userService, IMovieService movieService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _movieService = movieService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        // Authenticated
        [Authorize]
        public async Task<IActionResult> BuyMovie()
        {
            //if (User.Identity.IsAuthenticated)
            //{

            //}
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchase)
        //{
        //    return View();
        //}

        [HttpGet]
        // Authenticated
        [Authorize]
        public async Task<IActionResult> BuyMovie(int id)
        {
            //if (User.Identity.IsAuthenticated)
            //{
            var movie = await _movieService.GetMovieAsync(id);
            //}
            
            return View(movie);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> BuyMovie(PurchaseRequestModel purchase)
        {
            await _userService.PurchaseMovie(purchase, _currentUserService.UserId.GetValueOrDefault());
            return Ok();
        }
    }
}
