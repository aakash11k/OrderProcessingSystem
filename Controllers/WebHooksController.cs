using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/webhooks")]
public class WebhooksController : ControllerBase
{
    [HttpPost]
    public IActionResult ReceiveWebhook([FromBody] object payload)
    {
        Console.WriteLine(payload);

        return Ok("Webhook Received");
    }
}