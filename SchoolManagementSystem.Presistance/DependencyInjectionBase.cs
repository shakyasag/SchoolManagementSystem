using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementSystem.Application.Common;
using Microsoft.AspNetCore.Authentication;
using SchoolManagementSystem.Infrastructures.Common;

namespace SchoolManagementSystem.Presistance
{
    public static class DependencyInjectionBase
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseLazyLoadingProxies();
                options.UseSqlServer(
                configuration.GetConnectionString("TravelDb")
               );
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
           

            return services;
        }

    }
}
