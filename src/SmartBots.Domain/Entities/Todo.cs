using SmartBots.Domain.Common;

namespace SmartBots.Domain.Entities
{
    public class Todo : BaseAuditableEntity
    {
        public string Text { get; set; }
        public bool Completed { get; set; }
        public TodoPriority Priority { get; set; }

        public Todo(string text, TodoPriority priority)
        {
            Text = text;
            Priority = priority;
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

    public enum TodoPriority
    {
        Low,
        Medium,
        High
    }
}
