using AutoMapper;
using MediatR;
using SmartBots.Application.Interfaces;
using SmartBots.Domain.Entities;

namespace SmartBots.Application.Features.TradingBots;
internal sealed class AddTradingBotCommandHandler : IRequestHandler<AddTradingBotCommand, Guid>
{
    private readonly ITradingBotRepository _tradingBotRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AddTradingBotCommandHandler(
        ITradingBotRepository tradingBotRepository, 
        IUnitOfWork unitOfWork, 
        IMapper mapper)
    {
        _tradingBotRepository = tradingBotRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(AddTradingBotCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<TradingBot>(request.Model);
        model.Id = Guid.NewGuid();

        await _tradingBotRepository.AddAsync(model, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return model.Id;
    }
}
