using BodetLink.EventArgs;

namespace BodetLink.Services;

public class ParserService : IHostedService
{
    private readonly CatcherService _catcher;
    private readonly ILogger<ParserService> _logger;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _catcher.MessageCompleted += OnMessageCompleted;
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _catcher.MessageCompleted -= OnMessageCompleted;
        
        return Task.CompletedTask;
    }
    
    private void OnMessageCompleted(object? sender, System.EventArgs e)
    {
        MessageCompletedEventArgs ev = (MessageCompletedEventArgs) e;
        
        _logger.LogDebug("Message received {message}", ev.Message);
    }

    public ParserService(CatcherService catcher, ILogger<ParserService> logger)
    {
        _catcher = catcher;
        _logger = logger;
    }
}