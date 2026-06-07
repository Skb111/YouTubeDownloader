using Hangfire;
using YouTubeDownloader.Application.Abstractions;
using YouTubeDownloader.Infrastructure;
using YouTubeDownloader.Persistence;
using YouTubeDownloader.Web.Hubs;
using YouTubeDownloader.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure();

builder.Services.AddScoped<IDownloadProgressNotifier, SignalRDownloadProgressNotifier>();

builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(
        builder.Configuration.GetConnectionString("HangfireConnection"));
});

builder.Services.AddHangfireServer();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

app.MapHub<DownloadProgressHub>("/downloadProgressHub");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Downloads}/{action=Index}/{id?}");

app.Run();