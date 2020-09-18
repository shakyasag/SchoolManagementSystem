using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagementSystem.API.Helper;
using SchoolManagementSystem.Application;
using SchoolManagementSystem.Application.Common;
using SchoolManagementSystem.Application.Common.Interface;
using SchoolManagementSystem.Infrastructures.Common;
using SchoolManagementSystem.Presistance;
using SchoolnManagementSystem.Domain.Entity;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace SchoolManagementSystem.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            var origins = Configuration["CorsUrl"].Split(",");
            services.AddCors(options =>
            {
                options.AddPolicy("BasePolicy",
                builder =>
                {
                    builder
                    .AllowAnyMethod()
                    .WithOrigins(origins)
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            services.AddApplication();
            services.AddInfrastructure(Configuration);

            services.AddControllers(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Management System", Version = "v1" });
            });
            var architectureFolder = (IntPtr.Size == 8) ? "64 bit" : "32 bit";
            var wkHtmlToPdfPath = Path.Combine(Environment.ContentRootPath, $"libwkhtmltox");

            CustomAssemblyLoadContext context = new CustomAssemblyLoadContext();
            //context.LoadUnmanagedLibrary(wkHtmlToPdfPath);
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            IdentityBuilder builder = services.AddIdentityCore<User>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

            });
            builder = new IdentityBuilder(builder.UserType, typeof(Role), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>();
            builder.AddRoleValidator<RoleValidator<Role>>();
            builder.AddRoleManager<RoleManager<Role>>();
            builder.AddSignInManager<SignInManager<User>>();
            //services.AddCors();
            services.AddHealthChecks()
               .AddDbContextCheck<ApplicationDbContext>();
            services.AddHttpContextAccessor();

            services.AddControllersWithViews(); 

            services.AddRazorPages();
            services.AddTransient<IDateTimeServices, DateTimeServices>();
            services.AddScoped<IUserAccessor, UserAccessor>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            services.AddScoped<IRefreshTokens, RefreshTokens>();
            services.AddAuthentication(x=> {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
             .AddJwtBearer(options =>
             {
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
                         .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     RequireExpirationTime = false,
                     ValidateLifetime = true,
                     ClockSkew = TimeSpan.Zero

                 };
             });

        }
        internal class CustomAssemblyLoadContext : AssemblyLoadContext
        {
            public IntPtr LoadUnmanagedLibrary(string absolutePath)
            {
                return LoadUnmanagedDll(absolutePath);
            }

            protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
            {
                return LoadUnmanagedDllFromPath(unmanagedDllName);
            }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                throw new NotImplementedException();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseCustomExceptionHandler();
            app.UseHttpsRedirection();
            app.UseHealthChecks("/health");

            app.UseRouting();

            IdentityModelEventSource.ShowPII = true;
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "School Management System V1");
            });
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            app.UseAuthentication();
            //app.UseIdentityServer();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
