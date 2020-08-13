using System;

namespace OrderService.Models
{
    public class OrderConfirmation
    {
        public string OrderId { get; set; } = default!;

        public DateTime? DeliveryDate { get; set; }

        public bool Confirmed { get; set; }

        public int BackorderCount { get; set; }

        public int RemainingCount { get; set; }
    }
}
