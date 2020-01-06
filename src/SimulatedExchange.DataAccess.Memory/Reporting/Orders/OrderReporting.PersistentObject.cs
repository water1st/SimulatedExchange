using System;

namespace SimulatedExchange.DataAccess.ReportingTransaction.Orders
{
    public partial class OrderReporting
    {
        public class PersistentObject : IEquatable<PersistentObject>
        {
            public string Id { get; set; }
            //币对
            public string FromCurrencySymbol { get; set; }
            //币对
            public string ToCurrencySymbol { get; set; }
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
            //创建时间
            public DateTime CreatedTimeUtc { get; set; }
            //修改时间
            public DateTime ModifyedTimeUtc { get; set; }

            public bool Equals(PersistentObject other)
            {
                if (!ReferenceEquals(other, null))
                {
                    return other.Id == Id;
                }
                return false;
            }

            public override bool Equals(object obj)
            {
                if (!ReferenceEquals(obj, null))
                {
                    if (obj is PersistentObject other)
                    {
                        return Equals(other);
                    }
                }
                return false;
            }

            public override int GetHashCode()
            {
                return Id.GetHashCode();
            }

            public static bool operator ==(PersistentObject x, PersistentObject y) => x.Equals(y);

            public static bool operator !=(PersistentObject x, PersistentObject y) => !x.Equals(y);
        }
    }
}
