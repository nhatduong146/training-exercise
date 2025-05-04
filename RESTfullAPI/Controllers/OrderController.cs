using Microsoft.AspNetCore.Mvc;

namespace RESTfullAPI.Controllers;

[ApiController]
[Route("api/v1")]
public class OrderController : ControllerBase
{
    [HttpGet("orders")]
    public ActionResult GetOrders(CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpGet("orders/{id}")]
    public ActionResult GetOrderById(Guid id)
    {
        return Ok();
    }

    [HttpGet("orders/{id}/products")]
    public ActionResult GetOrderProducts(Guid id)
    {
        return Ok();
    }

    [HttpGet("orders/{orderId}/products/{productId}")]
    public ActionResult GetOrderProductById(Guid orderId, Guid productId)
    {
        return Ok();
    }
}
