using AutoMapper;
using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;
using PassportChecker.Services.Interfaces;

namespace PassportChecker.Services.Implementations;
public class ReaderHostedService : BackgroundService
{
    private readonly DataUpdaterService _readerService;
    private int Hour { get; set; }
    private int Minute { get; set; }
    public ReaderHostedService(IConfiguration configuration, ApiDbContext dbContext, IMapper mapper)
    {
        Hour = Int32.Parse(configuration["ReaderTime:Hour"]);
        Minute = Int32.Parse(configuration["ReaderTime:Minute"]);
        _readerService = new DataUpdaterService(dbContext, mapper);
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            if (DateTime.Now.Hour == Hour && DateTime.Now.Minute == Minute && DateTime.Now.Second == 0 && DateTime.Now.Microsecond == 0)
            {
                _readerService.GetDataFromSource();
            }
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
