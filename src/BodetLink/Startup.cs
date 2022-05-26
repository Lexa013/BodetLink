using BodetLink;
using BodetLink.Options;
using BodetLink.Workers;
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
        services.AddSingleton<CatcherWorker>();
        services.AddTransient<ParserWorker>();

        services.AddHostedService<BodetLinkHostedService>();
    });

var app = builder.Build();

app.Run();