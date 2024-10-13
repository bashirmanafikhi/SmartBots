using SmartBots.Domain.Common;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Domain.Entities
{
    public class Todo : BaseAuditableEntity, IUserOwnedEntity
    {
        public string Text { get; set; }
        public bool Completed { get; set; }
        public string ApplicationUserId { get; set; }

        public Todo(string applicationUserId, string text)
        {
            ApplicationUserId = applicationUserId;
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
