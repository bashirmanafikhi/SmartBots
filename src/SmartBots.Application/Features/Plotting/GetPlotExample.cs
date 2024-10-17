using MediatR;
using ScottPlot;

namespace SmartBots.Application.Features
{
    public class GetPlotExample : IRequest<Plot>
    {

    }
    public class GetPlotExampleHandler : IRequestHandler<GetPlotExample, Plot>
    {
        public Task<Plot> Handle(GetPlotExample request, CancellationToken cancellationToken)
        {
            Plot myPlot = new();

            var prices = Generate.RandomOHLCs(30);
            myPlot.Add.Candlestick(prices);
            myPlot.Axes.DateTimeTicksBottom();

            return Task.FromResult(myPlot);
        }
    }
}
