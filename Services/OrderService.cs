using Microsoft.EntityFrameworkCore;
using OrderProcessingSystem.Data;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Services;

public class OrderService
{
    private readonly AppDbContext _context;
    private readonly WebhookService _webhookService;

    public OrderService(
     AppDbContext context,
     WebhookService webhookService)
    {
        _context = context;
        _webhookService = webhookService;
    }

    public async Task<Order> CreateOrderAsync(string customerName, decimal amount)
    {
        var order = new Order
        {
            CustomerName = customerName,
            Amount = amount,
            Status = "Pending",
            CreatedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddMinutes(2)
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order;
    }

    public async Task<List<Order>> GetOrdersAsync()
    {
        return await _context.Orders.ToListAsync();
    }

    public async Task CompleteOrderAsync(int orderId)
    {
        var order = await _context.Orders.FindAsync(orderId);

        if (order == null)
            return;

        order.Status = "Completed";

        await _context.SaveChangesAsync();

        await _webhookService.SendOrderCompletedWebhook(order);
    }
}