using Microsoft.AspNetCore.Http;
using SmartBots.Application.Interfaces;
using System.Security.Claims;

namespace SmartBots.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? Name => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

        public string? GetUserEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                var emailClaim = user.FindFirst(ClaimTypes.Email);
                return emailClaim?.Value;
            }
            return null;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }

        public IEnumerable<Claim>? GetUserClaims()
        {
            return _httpContextAccessor.HttpContext?.User?.Claims;
        }

        public Guid? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user?.Identity?.IsAuthenticated ?? false)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim != null)
                {
                    return Guid.TryParse(userIdClaim.Value, out var userId) ? userId : null;
                }
            }
            return null;
        }
    }
}
