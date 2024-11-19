using ApplicationCore.ServiceInterface;
using System.Linq;
using System.Security.Claims;

namespace Infrastructure.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // HttpContextAccessor
        public int? UserId => Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity != null &&
            _httpContextAccessor.HttpContext != null &&
            _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string UserName => _httpContextAccessor.HttpContext?.User.Identity?.Name;

        public string FullName => GetName();

        public string Email => throw new NotImplementedException();

        public string RemoteIpAddress => throw new NotImplementedException();

        public IEnumerable<string> Roles => throw new NotImplementedException();

        public string ProfilePictureUrl { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsAdmin => throw new NotImplementedException();

        public bool IsSuperAdmin => throw new NotImplementedException();

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            throw new NotImplementedException();
        }

        private string GetName()
        {
            var firstName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            var lastName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
            return $"{firstName} {lastName}";
            //return firstName + " " + lastName

        }
    }
}
