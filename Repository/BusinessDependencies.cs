using Application.Contract;
using Application.Factories;
using Application.OrderService.OrderCommandHandlers;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Domain.UnitOfWork;
using Infrastructure.Order.CommandRepositories;
using Infrastructure.Order.QueryRepositories;
using Infrastructure.Trade.QueryRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class BusinessDependencies
    {
        public static IServiceCollection DependencyHolder(this IServiceCollection services)
        {
            services.AddDbContext<TradeMatchingEngineContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=TradeMatchingEngine;Integrated Security=true;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
            services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
            services.AddScoped<IAddOrderCommandHandlers, AddOrderCommandHandlers>();
            services.AddScoped<ITradeCommandRepository, TradeCommandRepository>();
            //builder.Services.AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITradeQueryRespository, TradeQueryRepository>();
            services.AddSingleton<IStockMarketFactory, StockMarketFactory>();
            services.AddScoped<IModifieOrderCommandHandler, ModifieOrderCommandHandler>();
            services.AddScoped<ICancellOrderCommandHandler, CancellOrderCommandHandler>();
            services.AddScoped<ICancellAllOrdersCommandHandler, CancellAllOrdersCommandHandler>();
            return services;
        }
    }
}
