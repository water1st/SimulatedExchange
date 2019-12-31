using System;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public class UpdateOrderTransaction : IReportingTransaction
    {
        public string Id { get; set; }
        public decimal Volume { get; set; }
        public int Status { get; set; }
        //成交时间
        public DateTime DateTime { get; set; }
    }
}
