using Microsoft.AspNetCore.Connections;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PassportChecker.Services.Implementations;

public class ReaderHostedService: BackgroundService
{
    private readonly ReaderService _readerService;
    private int Hour {  get; set; }
    private int Minute { get; set; }
    public ReaderHostedService(IConfiguration configuration)
    {
        Hour = Int32.Parse(configuration["ReaderTime:Hour"]);
        Minute = Int32.Parse(configuration["ReaderTime:Minute"]);
        _readerService = new ReaderService();
            //var readerHour = builder.Configuration["ReaderTime:Hour"];
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(true)
        {
            if (DateTime.Now.Hour == Hour && DateTime.Now.Minute == Minute)
            {
                _readerService.GetDataFromSource();
            }
        }
        

        //await Task.CompletedTask;
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await base.StopAsync(cancellationToken);
    }
}
