﻿using SmartBots.Application.Common;
using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Entities;
using SmartBots.Domain.Enums;

namespace SmartBots.Application.Features.Todos
{
    public class TodoDto : BaseDto, IMapFrom<Todo>
    {
        public TodoDto() { }
        public TodoDto(string text)
        {
            Text = text;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
        public TodoPriority Priority { get; set; }
    }
}
