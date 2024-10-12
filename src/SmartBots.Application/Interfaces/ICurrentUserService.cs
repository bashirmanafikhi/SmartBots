using System.Security.Claims;

namespace SmartBots.Application.Interfaces
{
    public interface ICurrentUserService
    {
        string? Name { get; }

        string? GetUserEmail();

        bool IsAuthenticated();

        bool IsInRole(string role);

        IEnumerable<Claim>? GetUserClaims();

        Guid? GetUserId();
    }
}
