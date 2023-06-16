using Application.Contract.CommandHandlerContracts;
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
using Framework.Contracts;
using Framework.Contracts.Common;
using Framework.Contracts.Events;
using Framework.Contracts.UnitOfWork;
using Infrastructure.GenericServices;
using Infrastructure.Orders.CommandRepositories;
using Infrastructure.Orders.QueryRepositories;
using Infrastructure.Trades.CommandRepositories;
using Infrastructure.Trades.QueryRepositories;
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
                    (typeof(AddOrderCommandHandler),typeof(AddOrderCommand))
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

                regiterCommandHandlerWithDecorator(container,
                    commandType: item.Value.Item2,
                    handlerInterface: item.Key,
                    handlerConcreteType: item.Value.Item1);

                //var t = typeof(TransactionalCommandHandler<>).MakeGenericType(new Type[1] { item.Value.Item2 });
                //var c = t.Name;
                //container.Register(
                //    Component.For(item.Key).ImplementedBy(item.Value.Item1).Named(INTERNAL_CMD_HANDLER_NAME + item.Value.Item1.Name).LifeStyle.ScopedToNetServiceScope()
                //   , Component.For(item.Key).ImplementedBy(t)
                //    .DependsOn(Dependency.OnComponent(item.Key, INTERNAL_CMD_HANDLER_NAME + item.Value.Item1.Name)).LifeStyle.ScopedToNetServiceScope().IsDefault()
                //    );
            }
            container.Register(
                Component.For<IDbConnectionService>().ImplementedBy<DbConnectionManager>()
                .DependsOn(Dependency.OnValue<string>(
                "Server=.;Initial Catalog=TradeMatchingEngine;" +
                "Integrated Security=true;Persist Security Info=False;" +
                "MultipleActiveResultSets=False;Encrypt=False;" +
                "TrustServerCertificate=False;Connection Timeout=30;"))
                .LifeStyle.ScopedToNetServiceScope().Forward<ITransactionService>());
        }
        public static IServiceCollection DependencyHolder(this IServiceCollection services)
        {

            services.AddDbContextFactory<TradeMatchingEngineContext>((sp, ob) =>
            {
                ob.UseSqlServer(sp.GetRequiredService<IDbConnectionService>().GetConnectionAsync().GetAwaiter().GetResult());
                //options.UseSqlServer("Server=.;Initial Catalog=TradeMatchingEngine;Integrated Security=true;Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;Connection Timeout=30;");
            });

            services.AddScoped<IOrderCommandRepository, OrderCommandRepository>();
            services.AddScoped<ITradeCommandRepository, TradeCommandRepository>();
            services.AddScoped<IOrderQueryRepository, OrderQueryRepository>();
            services.AddScoped<ITradeQueryRespository, TradeQueryRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IStockMarketFactory, StockMarketFactory>();
            services.AddScoped<IDomainEventHandler<OrderCreated>, DomainEventHandler>();
            services.AddScoped<IDispatcher, GenericDispatcher>();
            services.AddScoped<IServiceFactory, ServiceFactory>();
            return services;
        }

        private static void regiterCommandHandlerWithDecorator(IWindsorContainer container,
           Type commandType, Type handlerInterface, Type handlerConcreteType)
        {
            var t = typeof(TransactionalCommandHandler<>).MakeGenericType(new Type[1] { commandType });
            container.Register(
                Component.For(handlerInterface).ImplementedBy(handlerConcreteType)
                .Named(INTERNAL_CMD_HANDLER_NAME + handlerConcreteType.Name).LifeStyle.ScopedToNetServiceScope()
               , Component.For(handlerInterface).ImplementedBy(t)
                .DependsOn(Dependency.OnComponent(handlerInterface, INTERNAL_CMD_HANDLER_NAME + handlerConcreteType.Name))
                .LifeStyle.ScopedToNetServiceScope().IsDefault()
                );
        }
    }
}
