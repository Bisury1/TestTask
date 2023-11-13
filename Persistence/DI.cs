using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestTask.Application;

namespace TestTask.Persistence
{
    public static class DI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["connection"];
            services.AddDbContext<FileApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IFileGroupDbContext>(provider => provider.GetRequiredService<FileApplicationDbContext>());
            services.AddScoped<IFileEntityDbContext>(provider => provider.GetRequiredService<FileApplicationDbContext>());
            services.AddScoped<ILinkDbContext>(provider => provider.GetRequiredService<FileApplicationDbContext>());
            services.AddScoped<ISaveFileChanger>(provider => provider.GetRequiredService<FileApplicationDbContext>());
            return services;
        }
        
        public static IServiceCollection AddPersistenceIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["DefaultConnection"];
            services.AddDbContext<AppUserDbContext>(options =>
                options.UseNpgsql(connectionString));
            services.AddIdentityCore<AppUser>()
                .AddEntityFrameworkStores<AppUserDbContext>();
            return services;
        }
    }
}
