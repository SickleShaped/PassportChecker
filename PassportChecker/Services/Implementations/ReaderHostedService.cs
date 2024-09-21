using AutoMapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations;
public class ReaderHostedService : BackgroundService
{
    private readonly DataUpdaterService _readerService;
    private readonly IServiceProvider _provider;
    private Thread thread;
    private int Hour { get; set; }
    private int Minute { get; set; }
    public ReaderHostedService(IConfiguration configuration, IServiceProvider provider, IMapper mapper)
    {
        Hour = Int32.Parse(configuration["ReaderTime:Hour"]);
        Minute = Int32.Parse(configuration["ReaderTime:Minute"]);
        _provider = provider;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        thread = new Thread(new ThreadStart(CheckTime));
        thread.Start();
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {}

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }

    private void CheckTime()
    {
        while (true)
        {
            if (DateTime.UtcNow.Hour == Hour && DateTime.UtcNow.Minute == Minute && DateTime.UtcNow.Second < 2)
            {
                using (var scope = _provider.CreateScope())
                {
                    var x = scope.ServiceProvider.GetRequiredService<IDataUpdaterService>();
                    x.GetDataFromSource();
                }
            }
        }
    }
}
