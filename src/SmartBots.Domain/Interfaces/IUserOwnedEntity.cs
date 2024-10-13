namespace SmartBots.Domain.Interfaces;

public interface IUserOwnedEntity
{
    string ApplicationUserId { get; set; }

}

public static class UserOwnedEntityExtensions
{
    public static void Authorize(this IUserOwnedEntity entity, Guid? currentUserId)
    {
        var isAuthorized = currentUserId.HasValue &&
            string.Equals(currentUserId.ToString(), entity.ApplicationUserId, StringComparison.InvariantCultureIgnoreCase);

        if (!isAuthorized)
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
    }
}
