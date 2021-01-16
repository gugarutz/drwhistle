using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using DrWhistle.Application;
using DrWhistle.Application.Common.Interfaces;
using DrWhistle.Infrastructure;
using DrWhistle.Infrastructure.Persistence;
using DrWhistle.Web.Services;
using DrWhistle.WebUI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace DrWhistle.WebUI
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
            services.AddApplication();

            services.AddInfrastructure(Configuration);

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddHttpContextAccessor();

            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot")));

            services.AddControllersWithViews(o =>
            {
                // o.Filters.Add<RequireTenantContextFilter>();
            })
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
            .AddRazorRuntimeCompilation();

            services.AddRazorPages(options =>
            {
                // Since we are using the route multitenant strategy we must add the
                // route parameter to the Pages conventions used by Identity.
                options.Conventions.AddAreaFolderRouteModelConvention("Identity", "/Account", model =>
                {
                    foreach (var selector in model.Selectors)
                    {
                        selector.AttributeRouteModel.Template =
                            AttributeRouteModel.CombineTemplates($"{{{Domain.Constants.RouteTenantParameterName}}}", selector.AttributeRouteModel.Template);
                    }
                });
            });

            services.DecorateService<LinkGenerator, AmbientValueLinkGenerator>(new List<string> { $"{Domain.Constants.RouteTenantParameterName}" });

            services.AddTransient<Microsoft.AspNetCore.Identity.UI.Services.IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMapper autoMapper, ILogger<Startup> logger)
        {
            try
            {
                autoMapper.ConfigurationProvider.AssertConfigurationIsValid();

                var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

                ApplicationDbContextSeed.UpdateDatabases(scopeFactory, logger).GetAwaiter().GetResult();

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Main");

                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }

                app.UseSerilogRequestLogging();

                app.UseHttpsRedirection();

                app.UseStaticFiles();

                app.UseRouting();

                app.UseMultiTenant();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: $"{{{Domain.Constants.RouteTenantParameterName}}}/{{controller=Home}}/{{action=Index}}/{{id?}}");

                    endpoints.MapControllerRoute(
                        name: "landing",
                        pattern: "{controller=Home}/{action=Index}");

                    endpoints.MapRazorPages();
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in Configure");
                throw;
            }
        }
    }
}