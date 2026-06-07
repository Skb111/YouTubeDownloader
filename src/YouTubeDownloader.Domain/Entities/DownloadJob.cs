using YouTubeDownloader.Domain.Common;
using YouTubeDownloader.Domain.Enums;

namespace YouTubeDownloader.Domain.Entities;

public class DownloadJob : BaseEntity
{
    public string VideoUrl { get; set; } = string.Empty;
    public string VideoTitle { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public string Format { get; set; } = "mp4";

    public DownloadStatus Status { get; set; } = DownloadStatus.Pending;

    public int Progress { get; set; }

    public long? TotalBytes { get; set; }
    public long DownloadedBytes { get; set; }

    public string? OutputFilePath { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime? CompletedAt { get; set; }
}