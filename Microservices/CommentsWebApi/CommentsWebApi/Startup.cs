using System.IO;
using BLL.Interfaces;
using BLL.Services;
using DAL.Interfaces;
using DAL.Repositories;
using DAL.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using BLL.POCOs;
using BLL.Middlewares;

namespace CommentsWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Dependency injection
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ExceptionHandlingMiddleware>();

            if (Convert.ToBoolean(Configuration["EnableCaching"]))
            {
                services.Decorate<ICommentRepository, CommentRepositoryCachingDecorator>(); // Scrutor
            }

            services.Configure<EmailData>(Configuration.GetSection("EmailData"));

            //services.AddDbContext<CommentContext>(options =>
            //    options.UseNpgsql(Configuration.GetConnectionString("CommentsDbConnectionString"), npgsqlOptions =>
            //    {
            //        npgsqlOptions.EnableRetryOnFailure(
            //            maxRetryCount: 10,
            //            maxRetryDelay: TimeSpan.FromSeconds(5),
            //            errorCodesToAdd: new List<string> { "4060" });
            //    }));

            services.AddDbContext<CommentContext>(options =>
                options.UseNpgsql(Environment.GetEnvironmentVariable("CommentsDbConnectionString"), npgsqlOptions =>
                {
                    npgsqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorCodesToAdd: new List<string> { "4060" });
                }));

            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddAutoMapper(typeof(AutoMapperProfile)); // AutoMapper

            services.AddMemoryCache();

            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });



            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CommentsWebApi", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CommentsWebApi v1"));
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseSession();
            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile($@"{Directory.GetCurrentDirectory()}\Logs\log.txt");

        }
    }
}
