using System;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderService.Models;

namespace OrderService.Controllers
{
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;

        public OrdersController( ILogger<OrdersController> logger)
        {

            _logger = logger;
        }


        [Topic("orderplaced")]
        [HttpPost("orderplaced")]
        public async Task PlaceOrder(Order order,[FromServices]DaprClient daprClient)
        {
            _logger.LogInformation("Got order {OrderId} for product {ProductId}", order.OrderId, order.ProductId);

            var state = await daprClient.GetStateEntryAsync<InventoryState>("statestore", order.ProductId.ToString());
            if (state.Value == null || state.Value.Remaining <= -10)
            {
                state.Value = new InventoryState { Remaining = 10 };
            }

            state.Value.Remaining--;
            _logger.LogInformation("Updated inventory for {ProductId} to {stocks} remaining", order.ProductId, state.Value.Remaining);

            OrderConfirmation confirmation = state.Value.Remaining >= 0 ?
                new OrderConfirmation
                {
                    OrderId = order.OrderId,
                    Confirmed = true,
                    DeliveryDate = DateTime.Now.AddDays(8),
                    RemainingCount = state.Value.Remaining
                } : new OrderConfirmation
                {
                    OrderId = order.OrderId,
                    Confirmed = false,
                    BackorderCount = -1 * state.Value.Remaining
                };

            await daprClient.PublishEventAsync("orderprocessed", confirmation);
            _logger.LogInformation("Processed Order {OrderId}", confirmation.OrderId);
        }
    }
}
