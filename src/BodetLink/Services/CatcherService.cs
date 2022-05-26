using System.IO.Ports;
using BodetLink.EventArgs;
using BodetLink.Options;
using Microsoft.Extensions.Options;

namespace BodetLink.Services;

public class CatcherService : IHostedService
{
    private readonly ILogger<CatcherService> _logger;
    private readonly IOptions<SerialPortOptions> _options;
    
    private SerialPort _serialPort;
    private string _tempMessage;

    public event EventHandler MessageCompleted;
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _serialPort.DataReceived += OnDataReceived;
        
        return Task.CompletedTask;
    }
    

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _serialPort.DataReceived -= OnDataReceived;
        
        return Task.CompletedTask;
    }
    
    private void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
    {
        var rawData = (sender as SerialPort)?.ReadLine();
        _tempMessage += rawData;

        while (_tempMessage.Contains('\u0002') && _tempMessage.Contains('\u0003'))
        {
            var startPos = _tempMessage.IndexOf('\u0002');
            var endPos = _tempMessage.IndexOf('\u0003');

            var message = _tempMessage[(startPos + 2)..endPos];

            _tempMessage = _tempMessage.Remove(0, endPos + 1);

            OnMessageCompleted(new()
            {
                Message = message
            });
        }
    }

    private void OnMessageCompleted(MessageCompletedEventArgs e)
    {
        EventHandler handler = MessageCompleted;
        handler?.Invoke(this, e);
    }

    public CatcherService(ILogger<CatcherService> logger, IOptions<SerialPortOptions> options)
    {
        _logger = logger;
        _options = options;

        _serialPort = new SerialPort()
        {
            PortName = _options.Value.PortName,
            BaudRate = _options.Value.BaudRate,
            Parity = _options.Value.Parity,
            DataBits = _options.Value.DataBits,
            StopBits = _options.Value.StopBits
        };
    }
}