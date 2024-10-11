using Microsoft.AspNetCore.Http;
using SmartBots.Application.Interfaces;
using System.Security.Claims;

namespace SmartBots.Infrastructure.Common
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
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
