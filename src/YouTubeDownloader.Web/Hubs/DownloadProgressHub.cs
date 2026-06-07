using Microsoft.AspNetCore.SignalR;

namespace YouTubeDownloader.Web.Hubs;

public class DownloadProgressHub : Hub
{
    public async Task JoinDownloadGroup(string jobId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"download-{jobId}");
    }
}