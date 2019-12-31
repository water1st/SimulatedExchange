using System.Collections.Generic;
using static SimulatedExchange.DataAccess.ReportingTransaction.GetOrdersTransactionResult;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public class GetOrdersTransaction : IReportingTransaction<GetOrdersTransactionResult>
    {
        public PagingOptions PagingOptions { get; set; }
    }

    public class GetOrdersTransactionResult : List<GetOrdersTransactionResultItem>
    {
        public PagingInfo PagingInfo { get; set; }

        public class GetOrdersTransactionResultItem
        {
            //orderid
            public string Id { get; set; }
            //client orderid
            public string ClientId { get; set; }
            //币对
            public string PairSymbols { get; set; }
            //委托价格 
            public decimal Price { get; set; }
            //委托总量
            public decimal TotalAmount { get; set; }
            //交易所
            public int Exchange { get; set; }
            //订单类型
            public int Type { get; set; }
            //订单状态
            public int Status { get; set; }
            //成交量
            public decimal Volume { get; set; }
        }
    }
}
