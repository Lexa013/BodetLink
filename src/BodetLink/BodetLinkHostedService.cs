using BodetLink.Workers;

namespace BodetLink;

public class BodetLinkHostedService : IHostedService
{
    private readonly CatcherWorker _catcherWorker;
    private readonly ParserWorker _parserWorker;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _catcherWorker.Start();
        _parserWorker.Start();
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _catcherWorker.Stop();
        _parserWorker.Stop();
        
        return Task.CompletedTask;
    }

    public BodetLinkHostedService(CatcherWorker catcherWorker, ParserWorker parserWorker)
    {
        _catcherWorker = catcherWorker;
        _parserWorker = parserWorker;
    }
}