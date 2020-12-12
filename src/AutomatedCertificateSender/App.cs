using System.Threading;
using System.Threading.Tasks;
using AutomatedCertificateSender;
using AutomatedCertificateSender.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class App : BackgroundService
{
    private readonly ILogger<App> _logger;
    private readonly IGoogleAuth _googleAuth;

    public App(ILogger<App> logger, IGoogleAuth googleAuth)
    {
        this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        this._googleAuth = googleAuth ?? throw new System.ArgumentNullException(nameof(googleAuth));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var started = false;

        while(!stoppingToken.IsCancellationRequested) {

            if (!started)
            {
                var startUrl = _googleAuth.GenerateStartUrl();
                BrowserLauncherUtil.OpenBrowser(startUrl);
            }

            await Task.Delay(1000, stoppingToken);

            started = true;
        }
        
    }
}