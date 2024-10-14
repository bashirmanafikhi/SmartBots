using AutoMapper;
using MediatR;
using SmartBots.Application.Common;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class GetAllTodosQueryHandler : IRequestHandler<GetAllTodosQuery, PaginationResponse<TodoDto>>
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

        public async Task<PaginationResponse<TodoDto>> Handle(GetAllTodosQuery query, CancellationToken cancellationToken)
        {
            var predicate = query.Criteria.GetPredicateAsExpression();
            var paging = query.Paging;

            return await _todoRepository.GetCurrentUserItemsWithPaginationAsync(
                predicate,
                paging,
                x => x.Priority,
                cancellationToken);
        }
    }
}
