namespace OrderProcessingSystem.Models;

public class AuditLog
{
    public int AuditLogId { get; set; }

    public int OrderId { get; set; }

    public string Action { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; }
}