using Microsoft.AspNetCore.SignalR;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Web.Hubs;

namespace YouTubeDownloader.Web.Services;

public class SignalRDownloadProgressNotifier : IDownloadProgressNotifier
{
    private readonly IHubContext<DownloadProgressHub> _hubContext;

    public SignalRDownloadProgressNotifier(IHubContext<DownloadProgressHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyProgressAsync(
        Guid jobId,
        int progress,
        long downloadedBytes,
        long? totalBytes,
        string status)
    {
        await _hubContext.Clients
            .Group($"download-{jobId}")
            .SendAsync("ReceiveDownloadProgress", new
            {
                jobId,
                progress,
                downloadedBytes,
                totalBytes,
                status
            });
    }
}