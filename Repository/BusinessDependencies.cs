﻿using Application.Contract.CommandHandlerContracts;
using Application.Contract.Commands;
using Application.EventHandlers;
using Application.Factories;
using Application.OrderService.OrderCommandHandlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Extensions.DependencyInjection.Extensions;
using Domain.Contract.Orders.Repository.Command;
using Domain.Contract.Orders.Repository.Query;
using Domain.Contract.Trades.Repository.Command;
using Domain.Contract.Trades.Repository.Query;
using Domain.Events;
using Framework.Contracts.Events;
using Framework.Contracts.UnitOfWork;
using Infrastructure.Orders.CommandRepositories;
using Infrastructure.Orders.QueryRepositories;
using Infrastructure.Trades.CommandRepositories;
using Infrastructure.Trades.QueryRepositories;
using L02Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class BusinessDependencies
    {
        const string INTERNAL_CMD_HANDLER_NAME = "internalCmdHadler";
        public static void WindsorDependencyHolder(this IWindsorContainer container)
        {
            foreach (var item in new Dictionary<Type, (Type, Type)> {
                {
                    typeof(ICommandHandler<AddOrderCommand>),
                    (typeof(AddOrderCommandHandlers),typeof(AddOrderCommand))
                },
                {
                    typeof(ICommandHandler<ModifieOrderCommand>),
                    (typeof(ModifieOrderCommandHandler), typeof(ModifieOrderCommand))
                },
                {
                    typeof(ICommandHandler<CancelOrderCommand>),
                    (typeof(CancellOrderCommandHandler), typeof(CancelOrderCommand))
                },
                {
                    typeof(ICommandHandler<CancelAllOrderCommand>),
                    (typeof(CancellAllOrdersCommandHandler), typeof(CancelAllOrderCommand))
                },
                })
            {
                var t = typeof(TransactionalCommandHandler<>).MakeGenericType(new Type[1] { item.Value.Item2 });
                container.Register(
                    Component.For(item.Key).ImplementedBy(item.Value.Item1).Named(INTERNAL_CMD_HANDLER_NAME + item.Value.Item1.Name).LifeStyle.ScopedToNetServiceScope()
                   , Component.For(item.Key).ImplementedBy(t)
                    .DependsOn(Dependency.OnComponent(item.Key, INTERNAL_CMD_HANDLER_NAME + item.Value.Item1.Name)).LifeStyle.ScopedToNetServiceScope().IsDefault()
                    );
            }
        }
        public static IServiceCollection DependencyHolder(this IServiceCollection services)
        {
            services.AddDbContext<TradeMatchingEngineContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=TradeMatchingEngine;Integrated Security=true;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
            services.AddScoped<ITradeCommandRepository, TradeCommandRepository>();
            services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
            services.AddScoped<ITradeQueryRespository, TradeQueryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IStockMarketFactory, StockMarketFactory>();
            services.AddScoped<IEventHandler<OrderCreated>, DomainEventHandler>();
            services.AddScoped<IDispatcher<OrderCreated>, GenericDispatche<OrderCreated>>();
            return services;
        }
    }
}
