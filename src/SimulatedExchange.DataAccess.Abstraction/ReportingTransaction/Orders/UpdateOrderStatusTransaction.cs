using System;

namespace SimulatedExchange.DataAccess.ReportingTransaction
{
    public class UpdateOrderStatusTransaction : IReportingTransaction
    {
        public string Id { get; set; }
        public int Status { get; set; }
        //成交时间
        public DateTime DateTime { get; set; }
    }
}
