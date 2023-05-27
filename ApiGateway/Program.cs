using Facade.Contract;
using Messages;
using MessageFacadeProvider;
using Framework.Contracts;
using MessageNserviceBus;
using EndPoints.Controller;

namespace ApiGateway
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseNServiceBus(ctx =>
            {
                var endpointConfiguration = new EndpointConfiguration("ApiGateway");
                var transport = endpointConfiguration.UseTransport<LearningTransport>();

                var routing = transport.Routing();
                routing.RouteToEndpoint(typeof(AddOrderCommandMessage).Assembly, "StockMarketService");

                return endpointConfiguration;

            });

            

            builder.Services.AddScoped<IMessageService, MessageSender>();
            builder.Services.AddScoped<IOrderQueryFacade, ProxyOrderFacadeQueryService>();
            builder.Services.AddScoped<IOrderCommandFacade, OrderMessageCommandFacade>();
            builder.Services.AddScoped<ITradeQueryFacade, ProxyTradeFacadeQueryService>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddMvc();
            builder.Services
                    .AddControllers()
                    .AddApplicationPart(typeof(OrdersController).Assembly)
                    .AddControllersAsServices();



            var app=builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.MapControllers();
            app.Run();          
        }

    }
}