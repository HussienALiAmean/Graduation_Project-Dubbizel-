using Dubbizle.Models;
using Dubbizle.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Dubbizle.API.Config;
using Dubbizle.Mapper;

namespace Dubbizle.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(opt =>
                opt.RegisterModule(new AutofacModule()));

            builder.Services.AddAutoMapper(typeof(CategoryWithSubCategoryProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(SubCategoryProfile).Assembly);

            builder.Services.AddDbContext<Context>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
            .EnableSensitiveDataLogging());

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            

            app.MapControllers();

            app.Run();
        }
    }
}