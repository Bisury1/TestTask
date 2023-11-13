using AutoMapper;
using Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using TestTask.Application;
using TestTask.Application.Common.Mapping;
using TestTask.Persistence;

namespace TaskManager.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(config =>
            {
                config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
                config.AddProfile(new AssemblyMappingProfile(typeof(IFileGroupDbContext).Assembly));
            });
            var connectionString = Configuration["DefaultConnection"];
            services.AddDbContext<AppUserDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppUserDbContext>();
            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "/api/Auth/Login";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/api/Auth/Login";
            });

            services.AddMvc();
            services.AddApplication();
            services.AddPersistence(Configuration);
            services.AddSwaggerGen(options =>
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Description = "Demo Swagger API v1",
                Title = "Swagger",
                Version = "1.0.0"
            }));
            
            services.Configure<FormOptions>(x => {
                x.MultipartBodyLengthLimit = int.MaxValue;
            });

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger UI Demo");
                options.DocumentTitle = "Title";
                options.RoutePrefix = string.Empty;
                options.DocExpansion(DocExpansion.List);
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

        }
    }
}
