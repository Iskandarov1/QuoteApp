using App.Domain.Entities;
using App.Domain.Entities.Quots;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
namespace Services;

public class Worker(
    ILogger<Worker> logger,
    IServiceProvider serviceProvider) : BackgroundService
{
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);
    private readonly TimeSpan _retentionPeriod = TimeSpan.FromMinutes(1);


    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await CleanupOldQuotes(stoppingToken);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occured during the clean up");
            }
            await Task.Delay(_interval, stoppingToken);
        }
    }

     private async Task CleanupOldQuotes(CancellationToken cancellationToken) 
     {
         
            logger.LogInformation("Starting quote cleanup at {Time}", DateTimeOffset.UtcNow);
        
            using var scope = serviceProvider.CreateScope();
            var quoteRepository = scope.ServiceProvider.GetRequiredService<IQuoteRepository>();
        
            try
            {
                var cutoffDate = DateTime.UtcNow - _retentionPeriod;
                
                logger.LogDebug("Starting cleanup of quotes older than {CutoffDate}", cutoffDate);
                
                var deletedCount = await quoteRepository.DeleteQuotesOlderThanAsync(cutoffDate, cancellationToken);
        
                if (deletedCount > 0)
                {
                    logger.LogInformation("Successfully deleted {Count} quotes older than {Hours} hours", 
                        deletedCount, _retentionPeriod.TotalHours);
                }
                else
                {
                    logger.LogInformation("No quotes found older than {Minutes}", _retentionPeriod.Minutes);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to cleanup old quotes");
                throw;
            }
     }
     
}
