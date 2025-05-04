using ConcurrencyLab.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ConcurrencyLab.Controllers
{
    [ApiController]
    [Route("products")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            //var products = productService.GetProducts();
            var products = await productService.GetProductsAsync(cancellationToken);
            return Ok(products);
        }
    }
}
