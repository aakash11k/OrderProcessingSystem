namespace OrderProcessingSystem.Models;

public class Order
{
    public int OrderId { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string Status { get; set; } = "Pending";

    public DateTime CreatedDate { get; set; }

    public DateTime ExpiryDate { get; set; }
}