using EmployeeManager.ServerApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using AutoMapper;
using EmployeeManager.ServerApp.Mappings;
using System.Linq;
using Microsoft.AspNetCore.Antiforgery;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace EmployeeManager.ServerApp
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .AddCommandLine(System.Environment.GetCommandLineArgs()
            .Skip(1).ToArray());
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EmployeeManagerConnection")));
            services.AddAutoMapper(typeof(MappingProfile));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot/app"))
            });
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpa(spa =>
            {
                string strategy = Configuration
                    .GetValue<string>("DevTools:ConnectionStrategy");
                if (strategy == "proxy")
                {
                    spa.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200");
                }
                else if (strategy == "managed")
                {
                    spa.Options.SourcePath = "..\\..\\EmployeeManager.ClientApp";
                    spa.Options.StartupTimeout = new TimeSpan(0, 55, 0);
                    spa.UseAngularCliServer("start");
                }
            });
            SeedDb.SeedDatabase(services.GetRequiredService<DataContext>());

            if ((Configuration["INITDB"] ?? "false") == "true")
            {
                System.Console.WriteLine("Preparing Database...");
                SeedDb.SeedDatabase(services.GetRequiredService<DataContext>());
                System.Console.WriteLine("Database Preparation Complete");
                lifetime.StopApplication();
            }
        }
    }
}
