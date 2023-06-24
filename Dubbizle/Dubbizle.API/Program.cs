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

using Dubbizle.Mapper;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Dubbizle.API.Hubs;

namespace Dubbizle.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddSignalR();

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

            // for json
            builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

            builder.Host.ConfigureContainer<ContainerBuilder>(opt =>
                opt.RegisterModule(new AutofacModule()));
           // builder.Services.AddAutoMapper(typeof(ProfileMap).Assembly);

            builder.Services.AddAutoMapper(typeof(ProfileMap).Assembly);

            builder.Services.AddAutoMapper(typeof(CategoryWithSubCategoryProfile).Assembly);
            builder.Services.AddAutoMapper(typeof(SubCategoryProfile).Assembly);

            builder.Services.AddDbContext<Context>(opt =>
           opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
           .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .LogTo(log => Debug.WriteLine(log), LogLevel.Information)
           .EnableSensitiveDataLogging()
           );
            //signalR
            builder.Services.AddSignalR();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>();

            builder.Services.AddAuthentication(options =>
            {
                //jwt
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;//not valid account
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //parmeter
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:ValidIss"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAud"],
                    IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecrytKey"]))//asdZXCZX!#!@342352
                };
            })  //how to check if toke valid or not    
              ;


            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();


            builder.Services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ASP.NET 5 Web API",
                    Description = " ITI Projrcy"
                });
                // To Enable authorization using Swagger (JWT)    
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
             new string[] { }

                    }
                });
            });


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

            app.UseStaticFiles();
            app.UseCors("MyPolicy");
            app.UseAuthentication();

            app.MapHub<ReviewHub>("/Review");

            app.UseAuthorization();


            app.MapHub<ChatHub>("/ChatHub");
            app.MapControllers();

            app.Run();
        }
    }
}