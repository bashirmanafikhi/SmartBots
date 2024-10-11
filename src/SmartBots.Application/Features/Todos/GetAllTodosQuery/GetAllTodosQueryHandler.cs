using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Data.Models;

namespace SmartBots.Application.Features.Todos
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, List<TodoDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllTodosQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<TodoDto>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
        {
            return await _unitOfWork
                .Repository<Todo>()
                .ProjectAllToAsync<TodoDto>(_mapper.ConfigurationProvider, cancellationToken);
        }
    }
}
