using BodetLink.Workers;

namespace BodetLink;

public class BodetLinkHostedService : IHostedService
{
    private readonly CatcherWorker _catcherWorker;
    private readonly ParserWorker _parserWorker;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        var catcher = _catcherWorker;
        var parser = _parserWorker;

        catcher.MessageCompleted += parser.OnMessageCompleted;
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        var catcher = _catcherWorker;
        var parser = _parserWorker;
        
        catcher.MessageCompleted -= parser.OnMessageCompleted;
        
        return Task.CompletedTask;
    }

    public BodetLinkHostedService(CatcherWorker catcherWorker, ParserWorker parserWorker)
    {
        _catcherWorker = catcherWorker;
        _parserWorker = parserWorker;
    }
}