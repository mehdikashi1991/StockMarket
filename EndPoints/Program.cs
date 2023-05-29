using EndPoints;
using Infrastructure;
using NServiceBus;
using System;
using NServiceBus.Unicast.Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;


namespace EndPoints
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            // var builder = WebApplication.CreateBuilder(args).Build().Run();
            using (var outboardhost = CreateOutboardHost(args).Build())
            using (var internalhost = CreateInternalHost(Host.CreateDefaultBuilder(args)).Build())
            {
                await Task.WhenAll(outboardhost.StartAsync(), internalhost.StartAsync());
                await Task.WhenAll(outboardhost.WaitForShutdownAsync(), internalhost.WaitForShutdownAsync());
            }
        }

        public static IHostBuilder CreateOutboardHost(string[] args)
        {           
           
            var builder= Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }
            );


            return builder;


        }
        public static IHostBuilder CreateInternalHost(IHostBuilder builder)
        {
            builder.UseNServiceBus(ctx =>
            {
                var endpointconfig = new EndpointConfiguration("MessageHandlers");
                

                endpointconfig.UseTransport(new LearningTransport());

                return endpointconfig;
            });

            return builder;
        }

        //// Add services to the container.
        //builder.Services.AddRazorPages();
        //builder.Services.DependencyHolder();
        //builder.Services.AddControllers();
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen();

        //var app = builder.Build();


        //if (app.Environment.IsDevelopment())
        //{
        //    app.UseSwagger();
        //    app.UseSwaggerUI(options =>
        //    {
        //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        //        options.RoutePrefix = string.Empty;
        //    });
        //}



        //app.UseHttpsRedirection();
        //app.UseStaticFiles();

        //app.UseRouting();
        //app.UseEndpoints(endpoints => endpoints.MapControllers());
        //app.UseAuthorization();

        //app.Run();



    }
}