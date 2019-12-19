namespace SimulatedExchange.Api.DTO
{
    public class OrderDealRequest
    {
        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Amount { get; set; }
    }
}
