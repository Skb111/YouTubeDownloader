using Microsoft.Extensions.DependencyInjection;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Infrastructure.Services;

namespace YouTubeDownloader.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IVideoMetadataService, YouTubeMetadataService>();
        services.AddScoped<IVideoDownloadService, VideoDownloadService>();

        return services;
    }
}