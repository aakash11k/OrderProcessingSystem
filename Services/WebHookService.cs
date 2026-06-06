using System.Text;
using System.Text.Json;
using OrderProcessingSystem.Data;
using OrderProcessingSystem.Models;

namespace OrderProcessingSystem.Services;

public class WebhookService
{
    private readonly HttpClient _httpClient;
    private readonly AppDbContext _context;

    public WebhookService(
        HttpClient httpClient,
        AppDbContext context)
    {
        _httpClient = httpClient;
        _context = context;
    }

    public async Task SendOrderCompletedWebhook(Order order)
    {
        var payload = JsonSerializer.Serialize(new
        {
            order.OrderId,
            order.CustomerName,
            order.Amount,
            order.Status
        });

        var content = new StringContent(
            payload,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(
      "https://localhost:7007/api/webhooks",
      content);

        var responseText = await response.Content.ReadAsStringAsync();

        var status = response.IsSuccessStatusCode
    ? "Success"
    : "Failed";

        _context.WebhookLogs.Add(new WebhookLog
        {
            OrderId = order.OrderId,
            Payload = payload,
            Response = responseText,
            Status = status,
            RetryCount = 0,
            CreatedDate = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
    }
}