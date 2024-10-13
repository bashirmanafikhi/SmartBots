using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTodosQueryHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<List<TodoDto>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
        {
            return await _todoRepository.GetCurrentUserItems(cancellationToken);
        }
    }
}
