using System.Threading.Tasks;
using InventoryService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetProductsAsync()
        {
            var products = await _mediator.Send(new GetProductsQuery());
            return Ok(products);
        }
    }
}
