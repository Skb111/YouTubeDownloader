namespace YouTubeDownloader.Application.Abstractions;

public interface IDownloadProgressNotifier
{
    Task NotifyProgressAsync(
        Guid jobId,
        int progress,
        long downloadedBytes,
        long? totalBytes,
        string status);
}