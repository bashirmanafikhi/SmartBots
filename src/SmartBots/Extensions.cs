using MediatR;
using SmartBots.Application.Common.Behaviours;

namespace SmartBots;
public static class Extensions
{
    public static IServiceCollection AddBehaviors(this IServiceCollection services)
    {

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        return services;
    }
}
