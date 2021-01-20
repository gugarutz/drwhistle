using System;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Application.Common.Models;
using DrWhistle.Domain.Entities;
using DrWhistle.Infrastructure.Identity;
using DrWhistle.Infrastructure.Persistence;
using DrWhistle.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DrWhistle.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var emailSettingsSection = configuration.GetSection("EmailSettings");
            services.Configure<EmailSettings>(emailSettingsSection);

            services.AddTransient<IEmailSender, EmailSenderService>();

            bool useInMemoryDatabase = configuration.GetValue<bool>("UseInMemoryDatabase");

            if (useInMemoryDatabase)
            {
                services.AddDbContext<ApplicationDbContext>(
                    (sp, optionsBuilder) =>
                    {
                        var acc = sp.GetService<Finbuckle.MultiTenant.IMultiTenantContextAccessor<Tenant>>();

                        var tenantName = acc?.MultiTenantContext?.TenantInfo?.Identifier ?? acc?.MultiTenantContext?.TenantInfo?.Name ?? Guid.NewGuid().ToString();

                        optionsBuilder.UseInMemoryDatabase($"DrWhistle_{tenantName}");
                    });
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(
                    (sp, optionsBuilder) =>
                {
                    var acc = sp.GetService<Finbuckle.MultiTenant.IMultiTenantContextAccessor<Tenant>>();

                    var connStr = acc?.MultiTenantContext?.TenantInfo?.ConnectionString;

                    if (!string.IsNullOrWhiteSpace(connStr))
                    {
                        optionsBuilder.UseSqlServer(connStr);
                    }
                });
            }

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddIdentity<ApplicationUser, Role>(o =>
            {
                o.SignIn.RequireConfirmedEmail = true;
                o.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            // Register to use the database context and TTenantInfo types show above.
            services.AddMultiTenant<Tenant>()
                    .WithConfigurationStore()
                    .WithRouteStrategy()
                    .WithPerTenantOptions<CookieAuthenticationOptions>((options, tenantInfo) =>
                    {
                        options.Cookie.Path = $"/{tenantInfo.Identifier}";
                        options.Cookie.Name = $"{tenantInfo.Id}_auth";
                        options.Cookie.HttpOnly = true;
                        options.Cookie.Expiration = TimeSpan.FromMinutes(60);

                        options.LoginPath = $"/{tenantInfo.Identifier}/Identity/Account/Login";
                        options.LogoutPath = $"/{tenantInfo.Identifier}/Identity/Account/Logout";
                        options.AccessDeniedPath = $"/{tenantInfo.Identifier}/Identity/Account/AccessDenied";

                        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                        options.SlidingExpiration = true;
                    })
                    .WithPerTenantAuthentication();

            services.AddTransient<ITenantService<Tenant>, TenantService<Tenant>>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            return services;
        }
    }
}