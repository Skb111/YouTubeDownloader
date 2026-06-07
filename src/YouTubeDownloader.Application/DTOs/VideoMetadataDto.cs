namespace YouTubeDownloader.Application.DTOs;

public class VideoMetadataDto
{
    public string Url { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string ThumbnailUrl { get; set; } = string.Empty;
    public TimeSpan? Duration { get; set; }
}