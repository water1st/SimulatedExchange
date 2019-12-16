using System.Collections.Generic;

namespace SimulatedExchange.Api.DTO
{
    public class GerOrderListResponse : List<OrderListItem>
    {
        public CurrentPageInfo PageInfo { get; set; }
    }
}
