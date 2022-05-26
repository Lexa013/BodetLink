using BodetLink.EventArgs;

namespace BodetLink.Workers;

public class ParserWorker
{
    private readonly ILogger<ParserWorker> _logger;
    
    
    public void OnMessageCompleted(object? sender, System.EventArgs e)
    {
        MessageCompletedEventArgs ev = (MessageCompletedEventArgs) e;
        
        _logger.LogDebug("Message received {message}", ev.Message);
    }

    public ParserWorker(ILogger<ParserWorker> logger)
    {
        _logger = logger;
    }
}