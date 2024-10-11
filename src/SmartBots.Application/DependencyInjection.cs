using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SmartBots.Application.Features.Todos;
using System.Reflection;

namespace SmartBots.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddFluentValidation();
            services.AddAutoMapper();
            services.AddMediator();
            return services;
        }

        private static void AddFluentValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<AddTodoCommandValidator>();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
        private static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        private static void AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}
