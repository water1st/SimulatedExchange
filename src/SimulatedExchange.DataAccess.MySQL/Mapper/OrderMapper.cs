using SimulatedExchange.DataAccess.ReportingTransaction.Orders;
using SimulatedExchange.DataAccess.ReportingTransaction;

namespace SimulatedExchange.DataAccess.Mapper
{
    internal class OrderMapper : IOrderMapper
    {
        public GetOrdersTransactionResult.GetOrdersTransactionResultItem MapToGetOrdersTransactionResultItem(OrderReporting.PersistentObject persistentObject)
        {
            var result = new GetOrdersTransactionResult.GetOrdersTransactionResultItem();

            result.ClientId = persistentObject.ClientId;
            result.Exchange = persistentObject.Exchange;
            result.Id = persistentObject.Id;
            result.PairSymbols = $"{persistentObject.ToCurrencySymbol}-{persistentObject.FromCurrencySymbol}";
            result.Price = persistentObject.Price;
            result.Status = persistentObject.Status;
            result.TotalAmount = persistentObject.TotalAmount;
            result.Type = persistentObject.Type;
            result.Volume = persistentObject.Volume;

            return result;
        }

        public GetOrderTransactionResult MapToGetOrderTransactionResult(OrderReporting.PersistentObject persistentObject)
        {
            var result = new GetOrderTransactionResult();

            result.ClientId = persistentObject.ClientId;
            result.Exchange = persistentObject.Exchange;
            result.Id = persistentObject.Id;
            result.PairSymbols = $"{persistentObject.ToCurrencySymbol}-{persistentObject.FromCurrencySymbol}";
            result.Price = persistentObject.Price;
            result.Status = persistentObject.Status;
            result.TotalAmount = persistentObject.TotalAmount;
            result.Type = persistentObject.Type;
            result.Volume = persistentObject.Volume;

            return result;
        }
    }
}
