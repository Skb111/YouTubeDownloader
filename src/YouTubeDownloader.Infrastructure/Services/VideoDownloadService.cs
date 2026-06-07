using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Domain.Enums;
using YoutubeExplode;

namespace YouTubeDownloader.Infrastructure.Services;

public class VideoDownloadService : IVideoDownloadService
{
    private readonly IDownloadJobRepository _jobRepository;
    private readonly IDownloadProgressNotifier _progressNotifier;
    private readonly YoutubeClient _youtubeClient = new();

    public VideoDownloadService(
        IDownloadJobRepository jobRepository,
        IDownloadProgressNotifier progressNotifier)
    {
        _jobRepository = jobRepository;
        _progressNotifier = progressNotifier;
    }

    public async Task ProcessDownloadAsync(Guid jobId)
    {
        var job = await _jobRepository.GetByIdAsync(jobId);

        if (job == null)
            return;

        try
        {
            job.Status = DownloadStatus.Downloading;
            job.Progress = 0;
            job.DownloadedBytes = 0;
            job.ErrorMessage = null;

            await _jobRepository.UpdateAsync(job);

            await _progressNotifier.NotifyProgressAsync(
                job.Id,
                0,
                0,
                job.TotalBytes,
                "Downloading");

            var manifest = await _youtubeClient.Videos.Streams.GetManifestAsync(job.VideoUrl);

            var streamInfo = manifest
                .GetMuxedStreams()
                .Where(x => x.Container.Name == "mp4")
                .OrderByDescending(x => x.VideoQuality.MaxHeight)
                .FirstOrDefault();

            if (streamInfo == null)
                throw new Exception("No downloadable MP4 stream was found for this video.");

            var totalBytes = streamInfo.Size.Bytes;

            job.TotalBytes = totalBytes;
            await _jobRepository.UpdateAsync(job);

            await _progressNotifier.NotifyProgressAsync(
                job.Id,
                0,
                0,
                totalBytes,
                "Downloading");

            var downloadsFolder = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "downloads");

            Directory.CreateDirectory(downloadsFolder);

            var safeTitle = string.Join("_", job.VideoTitle.Split(Path.GetInvalidFileNameChars()));
            var filePath = Path.Combine(downloadsFolder, $"{safeTitle}_{job.Id}.mp4");

            var lastProgressSent = -1;

            var progress = new Progress<double>(value =>
            {
                var currentProgress = Math.Min(99, (int)(value * 100));
                var downloadedBytes = (long)(totalBytes * value);

                if (currentProgress == lastProgressSent)
                    return;

                lastProgressSent = currentProgress;

                _ = _progressNotifier.NotifyProgressAsync(
                    job.Id,
                    currentProgress,
                    downloadedBytes,
                    totalBytes,
                    "Downloading");
            });

            await _youtubeClient.Videos.Streams.DownloadAsync(streamInfo, filePath, progress);

            var fileInfo = new FileInfo(filePath);

            job.Status = DownloadStatus.Completed;
            job.Progress = 100;
            job.DownloadedBytes = fileInfo.Length;
            job.TotalBytes = fileInfo.Length;
            job.OutputFilePath = $"/downloads/{Path.GetFileName(filePath)}";
            job.CompletedAt = DateTime.UtcNow;

            await _jobRepository.UpdateAsync(job);

            await _progressNotifier.NotifyProgressAsync(
                job.Id,
                100,
                fileInfo.Length,
                fileInfo.Length,
                "Completed");
        }
        catch (Exception ex)
        {
            job.Status = DownloadStatus.Failed;
            job.ErrorMessage = ex.Message;

            await _jobRepository.UpdateAsync(job);

            await _progressNotifier.NotifyProgressAsync(
                job.Id,
                job.Progress,
                job.DownloadedBytes,
                job.TotalBytes,
                "Failed");
        }
    }
}