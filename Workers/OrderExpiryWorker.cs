using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Data;

namespace OrderProcessingSystem.Workers
{
    public class OrderExpiryWorker:BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OrderExpiryWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var Scope = _scopeFactory.CreateScope();

                var context = Scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var expiredOrders = await context.Orders.Where(o => o.Status == "Pending" && o.ExpiryDate <= DateTime.UtcNow).ToListAsync();

                foreach(var order in expiredOrders)
                {
                    order.Status = "Cancelled";
                }
                await context.SaveChangesAsync();

                await Task.Delay(
                    TimeSpan.FromSeconds(30), stoppingToken);
            }
        }
    }
}
