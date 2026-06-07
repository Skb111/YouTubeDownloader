using System.ComponentModel.DataAnnotations;

namespace YouTubeDownloader.Web.ViewModels;

public class CreateDownloadVM
{
    [Required]
    [Display(Name = "YouTube URL")]
    public string VideoUrl { get; set; } = string.Empty;
}