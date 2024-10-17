using AutoMapper;
using MediatR;
using SmartBots.Application.Common;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Todos
{
    public class SearchTodosQueryHandler : IRequestHandler<SearchTodosQuery, PaginatedList<TodoDto>>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SearchTodosQueryHandler(IUnitOfWork unitOfWork, ITodoRepository todoRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _todoRepository = todoRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedList<TodoDto>> Handle(SearchTodosQuery query, CancellationToken cancellationToken)
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
