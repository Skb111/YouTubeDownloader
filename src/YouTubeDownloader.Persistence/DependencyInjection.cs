using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Persistence.Context;
using YouTubeDownloader.Persistence.Repositories;

namespace YouTubeDownloader.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IDownloadJobRepository, DownloadJobRepository>();

        return services;
    }
}