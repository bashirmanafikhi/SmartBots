using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Data.Models
{
    public class Todo : BaseAuditableEntity, IUserOwnedEntity
    {
        public string Text { get; set; }
        public bool Completed { get; set; }
        public Guid UserId { get; set; }

        public Todo(Guid userId, string text)
        {
            UserId = userId;
            Text = text;
        }

        public void Complete()
        {
            SetCompleted(true);
        }

        public void Uncomplete()
        {
            SetCompleted(false);
        }

        private void SetCompleted(bool completed)
        {
            Completed = completed;
        }
    }
}
