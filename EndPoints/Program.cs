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
        public static void Main(string[] args)
        {

            // var builder = WebApplication.CreateBuilder(args).Build().Run();
            //using (var clientHost = CreateClientHost(args).Build());
            //using (var clientHost = CreateInternalHost(args).Build());
            CreateClientHost(args).Build().Run();

        }

        public static IHostBuilder CreateClientHost(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            }
            );


        }
        //public static IHostBuilder CreateInternalHost(string[] args)
        //{
        //    return Host.CreateDefaultBuilder(args)
        //        .UseNServiceBus()
        //}

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