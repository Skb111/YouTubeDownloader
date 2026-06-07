namespace YouTubeDownloader.Application.Abstractions;

public interface IVideoDownloadService
{
    Task ProcessDownloadAsync(Guid jobId);
}