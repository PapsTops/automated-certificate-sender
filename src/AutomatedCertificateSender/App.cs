using System;
using System.Threading;
using System.Threading.Tasks;
using AutomatedCertificateSender;
using AutomatedCertificateSender.Models;
using AutomatedCertificateSender.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class App : BackgroundService
{
    private readonly ILogger<App> _logger;
    private readonly IGoogleAuth _googleAuth;
    private readonly GoogleAuthSettingsService _googleAuthOptions;

    public App(ILogger<App> logger, IGoogleAuth googleAuth, GoogleAuthSettingsService googleAuthOptions)
    {
        this._logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        this._googleAuth = googleAuth ?? throw new System.ArgumentNullException(nameof(googleAuth));
        this._googleAuthOptions = googleAuthOptions ?? throw new System.ArgumentNullException(nameof(googleAuthOptions));
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var started = false;

        while (!stoppingToken.IsCancellationRequested)
        {

            if (!started)
            {
                _logger.LogInformation("Checking if we need to login.");

                if (ShouldOpenBrowserForAuthentication())
                {
                    var startUrl = _googleAuth.GenerateStartUrl();

                    BrowserLauncherUtil.OpenBrowser(startUrl);

                    started = true;

                }
            }

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }

    }


    internal bool ShouldOpenBrowserForAuthentication()
    {
        var googleAuthOptions = _googleAuthOptions.CurrentValue;

        return string.IsNullOrEmpty(googleAuthOptions.AccessToken) ||
            string.IsNullOrEmpty(googleAuthOptions.RefreshToken);
    }
}