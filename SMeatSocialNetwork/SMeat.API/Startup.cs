using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SMeat.MODELS;
using Microsoft.EntityFrameworkCore;
using SMeat.DAL;
using Microsoft.AspNetCore.Identity;
using SMeat.MODELS.Models;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using SMeat.API.Helpers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SMeatSocialNetwork.API.Hubs;

namespace SMeat.API
{
    public class Startup
    {
        private const string TOKEN = "token";

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<JWTOptions>(Configuration.GetSection("Tokens"));

            //services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnectionSqlServer")));

            services.AddDbContext<ApplicationNpgsqlContext>(options =>
               options.UseNpgsql(Configuration.GetConnectionString("DefaultConnectionNpgsql")));

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder

                    .WithOrigins("http://localhost:3000")
                    .WithOrigins("https://smeat-web.herokuapp.com")                    
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            // Add framework services.
            services.AddTransient<IUnitOfWork, UnitOfWork>();




            //services.AddScoped<IApplicationContext, ApplicationContext>();
            //services.AddIdentity<User, Role>()
            //    .AddEntityFrameworkStores<ApplicationContext>()
            //    .AddDefaultTokenProviders()
            //    .AddRoleValidator<RoleValidator<Role>>()
            //    .AddRoleManager<RoleManager<Role>>()
            //    .AddSignInManager<SignInManager<User>>();

            services.AddScoped<IApplicationContext, ApplicationNpgsqlContext>();
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationNpgsqlContext>()
                .AddDefaultTokenProviders()
                .AddRoleValidator<RoleValidator<Role>>()
                .AddRoleManager<RoleManager<Role>>()
                .AddSignInManager<SignInManager<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                //options.Password.RequireNonAlphanumeric = false;
                //options.Password.RequireUppercase = true;
                //options.Password.RequireLowercase = false;
                //options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
            
            //JWT
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))                      
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (MessageReceivedContext context) =>
                    {
                        if (/*context.HttpContext.WebSockets.IsWebSocketRequest &&*/ context.Request.Query.ContainsKey(TOKEN))
                        {
                            // pull the bearer token out of the QueryString for WebSocket connections
                            //_logger.LogInformation($"{nameof(OnMessageReceived)} processing websocket querystring authentication");
                            context.Token = context.Request.Query[TOKEN];
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DefaultPolicy",policy);
                options.DefaultPolicy = policy;
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddMvcCore().AddFormatterMappings().AddJsonFormatters()
            .AddJsonOptions(options => {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            // Add Database Initializer
            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IDataBaseInitializer dbInitializer)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("CorsPolicy");
            
            app.UseExceptionHandler(
            options => {
                options.Run(
                async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "text/html";
                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if (ex != null)
                    {
                        var err = "{ Error:" + $"{ ex.Error.Message}, ErrorTrace: {ex.Error.StackTrace }"+"}";
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                });
            });


            app.UseAuthentication( );
            
            app.UseMvc();

            //app.UseIdentity();

            //Generate EF Core Seed Data
            ((DataBaseInitializer)dbInitializer).Initialize().Wait();

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
            });
        }
    }
}
