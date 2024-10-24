﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBots.Application.Interfaces;
using SmartBots.Infrastructure.Data;
using SmartBots.Infrastructure.Interceptors;
using SmartBots.Infrastructure.Repositories;
using SmartBots.Infrastructure.Services;

namespace SmartBots.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories();

            return services;
        }

        public static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<AuditableEntityInterceptor>();

            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString,
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .AddInterceptors(sp.GetRequiredService<AuditableEntityInterceptor>());

            }, contextLifetime:ServiceLifetime.Transient);
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();
        }

        private static void AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped(typeof(IUserOwnedEntityRepository<>), typeof(UserOwnedEntityRepository<>))

                .AddScoped<ITradingRuleRepository, TradingRuleRepository>()
                .AddScoped<ITodoRepository, TodoRepository>()
                .AddScoped<IExchangeAccountRepository, ExchangeAccountRepository>()
                .AddScoped<ITradingBotRepository, TradingBotRepository>()

                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddScoped<IExchangeFactory, ExchangeFactory>()
                .AddScoped<ITradingBotManager, TradingBotManager>()
                .AddScoped<IRealTimeDataManager, RealTimeDataManager>()
                .AddScoped<ITechnicalAnalysisService, TechnicalAnalysisService>()
                .AddScoped<ITradingRuleManager, TradingRuleManager>();

        }
    }
}
