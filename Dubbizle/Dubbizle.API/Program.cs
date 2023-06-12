using Dubbizle.Models;
using Dubbizle.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using Dubbizle.API.Config;
using Dubbizle.DTOs;
using Mapper;
using Microsoft.AspNetCore.Identity;

namespace Dubbizle.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add services to the container.
            builder.Services.AddCors(options => {
                options.AddPolicy("MyPolicy", builder =>
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            //builder.Host.ConfigureContainer<ContainerBuilder>(opt =>
            //    opt.RegisterModule(new AutofacModule()));
            //builder.Services.AddAutoMapper(typeof(ProfileMap).Assembly);

            //builder.Services.AddDbContext<Context>(opt =>
            //opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
            //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //.LogTo(log => Debug.WriteLine(log), LogLevel.Information)
            //.EnableSensitiveDataLogging());

            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(opt =>
                opt.RegisterModule(new AutofacModule()));
            builder.Services.AddAutoMapper(typeof(ProfileMap).Assembly);

            builder.Services.AddDbContext<Context>(opt =>
           opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
           .EnableSensitiveDataLogging()
           );


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();

            var app = builder.Build();
            app.UseCors("MyPolicy");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}