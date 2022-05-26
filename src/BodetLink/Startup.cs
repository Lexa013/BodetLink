using BodetLink.Options;
using BodetLink.Services;
using Serilog;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration(configurationBuilder =>
        configurationBuilder
            .AddJsonFile("appsettings.json", false, true))
    .UseSerilog((_, configuration) =>
    {
        configuration
            .Enrich.FromLogContext().WriteTo.Console();
    })
    .ConfigureServices((host, services) =>
    {
        // Options
        services.Configure<SerialPortOptions>(host.Configuration.GetSection("SerialPort"));

        // SignalR
        services.AddSignalR();
        services.AddCors(options =>
            options.AddDefaultPolicy(policyBuilder =>
            {
                policyBuilder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials();
            }));
        
        // SerialPort
        services.AddHostedService<CatcherService>();
        services.AddHostedService<ParserService>();
    });

var app = builder.Build();

app.Run();