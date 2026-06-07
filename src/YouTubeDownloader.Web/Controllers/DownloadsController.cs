using Hangfire;
using Microsoft.AspNetCore.Mvc;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Domain.Entities;
using YouTubeDownloader.Web.ViewModels;

namespace YouTubeDownloader.Web.Controllers;

public class DownloadsController : Controller
{
    private readonly IVideoMetadataService _metadataService;
    private readonly IDownloadJobRepository _jobRepository;

    public DownloadsController(
        IVideoMetadataService metadataService,
        IDownloadJobRepository jobRepository)
    {
        _metadataService = metadataService;
        _jobRepository = jobRepository;
    }

    public async Task<IActionResult> Index()
    {
        var jobs = await _jobRepository.GetAllAsync();
        return View(jobs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateDownloadVM model)
    {
        if (!ModelState.IsValid)
            return RedirectToAction(nameof(Index));

        var metadata = await _metadataService.GetMetadataAsync(model.VideoUrl);

        var job = new DownloadJob
        {
            VideoUrl = model.VideoUrl,
            VideoTitle = metadata.Title,
            ThumbnailUrl = metadata.ThumbnailUrl,
            Format = "mp4"
        };

        await _jobRepository.AddAsync(job);

        BackgroundJob.Enqueue<IVideoDownloadService>(
            service => service.ProcessDownloadAsync(job.Id));

        return RedirectToAction(nameof(Details), new { id = job.Id });
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var job = await _jobRepository.GetByIdAsync(id);

        if (job == null)
            return NotFound();

        return View(job);
    }
}