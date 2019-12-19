using System.Collections.Generic;

namespace SimulatedExchange.Api.DTO
{
    public class GerOrderListResponse : List<OrderListItem>
    {
        /// <summary>
        /// 分页信息
        /// </summary>
        public CurrentPageInfo PageInfo { get; set; }
    }
}
