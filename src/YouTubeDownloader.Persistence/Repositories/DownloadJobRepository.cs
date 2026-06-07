using Microsoft.EntityFrameworkCore;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Domain.Entities;
using YouTubeDownloader.Persistence.Context;

namespace YouTubeDownloader.Persistence.Repositories;

public class DownloadJobRepository : IDownloadJobRepository
{
    private readonly ApplicationDbContext _context;

    public DownloadJobRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(DownloadJob job)
    {
        await _context.DownloadJobs.AddAsync(job);
        await _context.SaveChangesAsync();
    }

    public async Task<DownloadJob?> GetByIdAsync(Guid id)
    {
        return await _context.DownloadJobs.FindAsync(id);
    }

    public async Task UpdateAsync(DownloadJob job)
    {
        _context.DownloadJobs.Update(job);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DownloadJob>> GetAllAsync()
    {
        return await _context.DownloadJobs
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }
}