namespace SmartBots.Domain.Interfaces;

public interface IUserOwnedEntity
{
    Guid UserId { get; set; }

}

public static class UserOwnedEntityExtensions
{
    public static void Authorize(this IUserOwnedEntity entity, Guid? currentUserId)
    {
        var isAuthorized = currentUserId.HasValue &&
            Guid.Equals(currentUserId, entity.UserId);

        if (!isAuthorized)
            throw new UnauthorizedAccessException("You are not authorized to access this resource.");
    }
}
