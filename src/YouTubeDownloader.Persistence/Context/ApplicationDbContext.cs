using Microsoft.EntityFrameworkCore;
using YouTubeDownloader.Domain.Entities;

namespace YouTubeDownloader.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<DownloadJob> DownloadJobs => Set<DownloadJob>();
}