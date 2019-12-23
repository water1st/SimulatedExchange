namespace SimulatedExchange.Api.DTO
{
    public class OrderListItem
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 客户端OrderId
        /// </summary>
        public string ClientId { get; set; }
        /// <summary>
        /// 币对：usdt-btc
        /// </summary>
        public string PairSymbols { get; set; }
        /// <summary>
        /// 委托价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 委托总量
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 成交量
        /// </summary>
        public decimal Volume { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int Status { get; set; }
    }
}
