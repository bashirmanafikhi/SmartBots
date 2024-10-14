using MediatR;
using ScottPlot;
using SmartBots.Application.Features.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            double[] sin = Generate.Sin(51);
            double[] cos = Generate.Cos(51);
            myPlot.Add.Signal(sin);
            myPlot.Add.Signal(cos);

            return Task.FromResult(myPlot);
        }
    }
}
