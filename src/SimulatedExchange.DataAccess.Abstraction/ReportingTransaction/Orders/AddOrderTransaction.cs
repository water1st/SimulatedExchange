using System;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public class AddOrderTransaction : IReportingTransaction
    {
        public string Id { get; set; }
        //币对
        public string Symbols { get; set; }
        //委托价格
        public decimal Price { get; set; }
        //委托总量
        public decimal Amount { get; set; }
        //交易所
        public int Exchange { get; set; }
        //订单类型
        public int Type { get; set; }
        //订单时间
        public DateTime DateTime { get; set; }
        public int Status { get; set; }
        public int Volume { get; set; }
    }
}
