namespace DemoApi;

public class BackgroundTask : IHostedService
{
    private readonly ILogger<BackgroundTask> _logger;

    public BackgroundTask(ILogger<BackgroundTask> logger)
    {
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        while(!cancellationToken.IsCancellationRequested)
        {
            await Task.Delay(1000);
            _logger.LogInformation("Background Task Checking on things...");
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
