using SmartBots.Domain.Common;

namespace SmartBots.Data.Models
{
    public class Todo : BaseAuditableEntity
    {
        public string Text { get; set; }
        public bool Completed { get; set; }

        public Todo(string text)
        {
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
