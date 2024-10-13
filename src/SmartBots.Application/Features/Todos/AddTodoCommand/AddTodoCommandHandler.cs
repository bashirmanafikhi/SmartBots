using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class AddTodoCommandHandler : IRequestHandler<AddTodoCommand, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AddTodoCommandHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<TodoDto> Handle(AddTodoCommand command, CancellationToken cancellationToken)
        {
            var todo = new Todo(command.Text);

            await _todoRepository.AddAsync(todo, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return _mapper.Map<TodoDto>(todo);
        }
    }

}
