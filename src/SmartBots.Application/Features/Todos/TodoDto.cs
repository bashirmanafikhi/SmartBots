using SmartBots.Application.Common.Mappings;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.Todos
{
    public class TodoDto : IMapFrom<Todo>
    {
        public TodoDto() { }
        public TodoDto(string text)
        {
            Text = text;
        }

        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool Completed { get; set; }
    }
}
