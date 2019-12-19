namespace SimulatedExchange.Api.DTO
{
    public class NewOrderRequest
    {
        /// <summary>
        /// 币对：usdt-btc
        /// </summary>
        public string PairSymbols { get; set; }
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
