using Microsoft.Extensions.DependencyInjection;
using TestTask.Application.Common.DownloadableFiles;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerFactory;
using TestTask.Application.Common.ProgressTrackerInfrastructure.ProgressTrackerRepository;

namespace TestTask.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DI).Assembly);
            });
            services.AddSingleton<IProgressTrackersRepository, ProgressTrackersRepository>();
            services.AddSingleton<IProgressTrackerFactory, FileProgressTrackerFactory>();
            services.AddSingleton<IDownloadableFilesRepository, DownloadableFilesRepository>();
            return services;
        }
    }
}
