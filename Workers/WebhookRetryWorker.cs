using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Data;

namespace OrderProcessingSystem.Workers;

public class WebhookRetryWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public WebhookRetryWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var context =
                scope.ServiceProvider
                     .GetRequiredService<AppDbContext>();

            var failedWebhooks =
                await context.WebhookLogs
                    .Where(x =>
                        x.Status == "Failed" &&
                        x.RetryCount < 3)
                    .ToListAsync();

            foreach (var webhook in failedWebhooks)
            {
                webhook.RetryCount++;

                Console.WriteLine(
                    $"Retrying Webhook {webhook.WebhookLogId}");
            }

            await context.SaveChangesAsync();

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }
    }
}