﻿using SmartBots.Domain.Common;
using SmartBots.Domain.Enums;
using SmartBots.Domain.Interfaces;

namespace SmartBots.Domain.Entities
{
    public class Todo : BaseAuditableEntity, IUserOwnedEntity
    {
        public string Text { get; set; }
        public bool Completed { get; set; }
        public string ApplicationUserId { get; set; }
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

        public void Update(string text, TodoPriority priority)
        {
            Text = text;
            Priority = priority;
        }
    }
}
