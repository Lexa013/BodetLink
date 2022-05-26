using BodetLink.EventArgs;
using BodetLink.SeedWork;

namespace BodetLink.Workers;

public class ParserWorker : Worker
{
    private readonly CatcherWorker _catcher;
    private readonly ILogger<ParserWorker> _logger;

    public override void Start()
    {
        _catcher.MessageCompleted += OnMessageCompleted;
    }

    public override void Stop()
    {
        _catcher.MessageCompleted -= OnMessageCompleted; ;
    }
    
    private void OnMessageCompleted(object? sender, System.EventArgs e)
    {
        MessageCompletedEventArgs ev = (MessageCompletedEventArgs) e;
        
        _logger.LogDebug("Message received {message}", ev.Message);
    }

    public ParserWorker(CatcherWorker catcher, ILogger<ParserWorker> logger)
    {
        _catcher = catcher;
        _logger = logger;
    }
}