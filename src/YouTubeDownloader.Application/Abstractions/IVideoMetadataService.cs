using YouTubeDownloader.Application.DTOs;

namespace YouTubeDownloader.Application.Abstractions;

public interface IVideoMetadataService
{
    Task<VideoMetadataDto> GetMetadataAsync(string videoUrl);
}