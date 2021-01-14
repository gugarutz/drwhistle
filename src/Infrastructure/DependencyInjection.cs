using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace DrWhistle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register the db context, but do not specify a provider/connection
            // string since these vary by tenant.
            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }
}
