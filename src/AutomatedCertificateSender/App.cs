using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

public class App : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested) {
            await Task.Delay(1000);
        }
        
    }
}