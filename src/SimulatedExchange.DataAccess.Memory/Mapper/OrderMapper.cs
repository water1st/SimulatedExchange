using SimulatedExchange.DataAccess.ReportingTransaction.Orders;
using SimulatedExchange.DataAccess.ReportingTransaction;
using System;

namespace SimulatedExchange.DataAccess.Mapper
{
    internal class OrderMapper : IOrderMapper
    {
        public OrderReporting.PersistentObject MapFromAddOrderTransaction(AddOrderTransaction transaction)
        {
            var symbol = transaction.Symbols.Split('-');
            var result = new OrderReporting.PersistentObject
            {
                ClientId = transaction.ClientId,
                CreatedTimeUtc = DateTime.UtcNow,
                Exchange = transaction.Exchange,
                FromCurrencySymbol = symbol[1],
                Id = transaction.Id,
                Price = transaction.Price,
                Status = 0,
                ToCurrencySymbol = symbol[0],
                TotalAmount = transaction.Amount,
                Type = transaction.Type,
                Volume = 0
            };

            return result;
        }

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
