using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouTubeDownloader.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class VideoSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DownloadedBytes",
                table: "DownloadJobs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "TotalBytes",
                table: "DownloadJobs",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownloadedBytes",
                table: "DownloadJobs");

            migrationBuilder.DropColumn(
                name: "TotalBytes",
                table: "DownloadJobs");
        }
    }
}
