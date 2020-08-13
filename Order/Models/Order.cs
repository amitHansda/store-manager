using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Models
{
    public class Order
    {
        public string OrderId { get; set; } = default!;
        public int ProductId { get; set; }

    }
}
