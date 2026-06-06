namespace OrderProcessingSystem.DTOs;

public class CreateOrderDto
{
    public string CustomerName { get; set; } = string.Empty;

    public decimal Amount { get; set; }
}