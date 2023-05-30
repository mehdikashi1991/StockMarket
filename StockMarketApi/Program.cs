using Facade.Contract;
using FacadeProvider.OrderFacadeProviders;
using FacadeProvider.TradeFacadeProvider;
using Infrastructure;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using NServiceBus.Extensions.Logging;

namespace StockMarketApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            try
            {
                LogManager
                .Setup()
                .LoadConfigurationFromAppSettings();

                var builder = WebApplication.CreateBuilder(args);

                //NLog
                builder.Host.ConfigureLogging((hostingContext, logging) =>
                {
                    logging.ClearProviders();
                    logging.AddNLog();
                    //LogManager.Configuration.Variables["HostKey"] = Assembly.GetExecutingAssembly().FullName!.Split(',')[0];
                });

                builder.Host.UseNServiceBus(ctx =>
                {
                    NServiceBus.Logging.LogManager.UseFactory(loggerFactory: new ExtensionsLoggerFactory(
                        builder.Services.BuildServiceProvider().
                        GetRequiredService<ILoggerFactory>())
                        );

                    var endpointConfiguration = new EndpointConfiguration("StockMarketService");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();

                    return endpointConfiguration;
                });

                builder.Services.DependencyHolder();
                builder.Services.AddScoped<IOrderCommandFacade, OrderCommandFacade>();
                builder.Services.AddScoped<IOrderQueryFacade, OrderQueryFacade>();
                builder.Services.AddScoped<ITradeQueryFacade, TradeQueryFacade>();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddMvc();

                var app = builder.Build();

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
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}