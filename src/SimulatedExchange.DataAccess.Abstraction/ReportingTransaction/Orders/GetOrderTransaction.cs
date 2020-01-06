using System;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public class GetOrderTransaction : IReportingTransaction<GetOrderTransactionResult>
    {
        public Guid Id { get; set; }
    }

    public class GetOrderTransactionResult
    {
        //订单id
        public string Id { get; set; }
        //币对
        public string PairSymbols { get; set; }
        //委托价格 
        public decimal Price { get; set; }
        //成交量
        public decimal Volume { get; set; }
        //委托总量
        public decimal TotalAmount { get; set; }
        //交易所
        public int Exchange { get; set; }
        //订单类型
        public int Type { get; set; }
        //订单状态
        public int Status { get; set; }
    }

}
