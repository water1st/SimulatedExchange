using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedExchange.Api.DTO
{
    public class OrderDealRequest
    {
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
