using YouTubeDownloader.Domain.Entities;

namespace YouTubeDownloader.Application.Abstractions;

public interface IDownloadJobRepository
{
    Task AddAsync(DownloadJob job);
    Task<DownloadJob?> GetByIdAsync(Guid id);
    Task UpdateAsync(DownloadJob job);
    Task<List<DownloadJob>> GetAllAsync();
}