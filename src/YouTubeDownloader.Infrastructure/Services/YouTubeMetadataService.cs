using YoutubeExplode;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Application.DTOs;

namespace YouTubeDownloader.Infrastructure.Services;

public class YouTubeMetadataService : IVideoMetadataService
{
    private readonly YoutubeClient _youtubeClient = new();

    public async Task<VideoMetadataDto> GetMetadataAsync(string videoUrl)
    {
        var video = await _youtubeClient.Videos.GetAsync(videoUrl);

        return new VideoMetadataDto
        {
            Url = videoUrl,
            Title = video.Title,
            Duration = video.Duration,
            ThumbnailUrl = video.Thumbnails.OrderByDescending(x => x.Resolution.Area).First().Url
        };
    }
}