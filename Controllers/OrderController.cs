using Microsoft.AspNetCore.Mvc;
using OrderProcessingSystem.DTOs;
using OrderProcessingSystem.Services;

namespace OrderProcessingSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderDto dto)
    {
        var order = await _orderService.CreateOrderAsync(
            dto.CustomerName,
            dto.Amount);

        return Ok(order);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await _orderService.GetOrdersAsync();

        return Ok(orders);
    }

    [HttpPut("{id}/complete")]
    public async Task<IActionResult> CompleteOrder(int id)
    {
        await _orderService.CompleteOrderAsync(id);

        return Ok("Order Completed");
    }

    [HttpPost("webhook")]
    public IActionResult ReceiveWebhook(
    [FromBody] object payload)
    {
        return Ok();
    }

}