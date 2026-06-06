namespace OrderProcessingSystem.Models;

public class WebhookLog
{
    public int WebhookLogId { get; set; }

    public int OrderId { get; set; }

    public string Payload { get; set; } = string.Empty;

    public string Response { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public int RetryCount { get; set; }
}