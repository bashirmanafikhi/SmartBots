using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using SmartBots.Application.Interfaces;

namespace SmartBots.Application.Features.Exchange
{
    public class GetAllExchangesQueryHandler : IRequestHandler<GetAllExchangesQuery, List<ExchangeDto>>
    {
        private readonly IExchangeRepository _exchangeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllExchangesQueryHandler(IUnitOfWork unitOfWork, IExchangeRepository exchangeRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
            _mapper = mapper;
        }

        public async Task<List<ExchangeDto>> Handle(GetAllExchangesQuery query, CancellationToken cancellationToken)
        {
            var exchangesQuery = _exchangeRepository.Query();

            var exchangeDtos = exchangesQuery
                .ProjectTo<ExchangeDto>(_mapper.ConfigurationProvider)
                .ToList();

            return exchangeDtos;
        }
    }
}
