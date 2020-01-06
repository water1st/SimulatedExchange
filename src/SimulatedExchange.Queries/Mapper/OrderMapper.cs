using SimulatedExchange.DataAccess.ReportingTransaction;
using SimulatedExchange.Queries.Orders;
using static SimulatedExchange.DataAccess.ReportingTransaction.GetOrdersTransactionResult;

namespace SimulatedExchange.Queries.Mapper
{
    internal class OrderMapper : IOrderMapper
    {
        public GetOrderQueryResult MapToGetOrderQueryResult(GetOrderTransactionResult item)
        {
            var result = new GetOrderQueryResult
            {
                Exchange = item.Exchange,
                Id = item.Id,
                PairSymbols = item.PairSymbols,
                Price = item.Price,
                Status = item.Status,
                TotalAmount = item.TotalAmount,
                Type = item.Type,
                Volume = item.Volume
            };

            return result;
        }

        public GetOrdersQueryResult MapToGetOrdersQueryResult(GetOrdersTransactionResult items)
        {
            var result = new GetOrdersQueryResult();
            foreach (var item in items)
            {
                var data = MaotpGetOrdersQueryResultItem(item);
                result.Add(data);
            }
            result.PagingInfo = MapToPagingInfo(items.PagingInfo);
            return result;
        }

        private PagingInfo MapToPagingInfo(DataAccess.ReportingTransaction.PagingInfo pagingInfo)
        {
            var result = new PagingInfo
            {
                CurrentPageIndex = pagingInfo.CurrentPageIndex,
                PageCount = pagingInfo.PageCount
            };

            return result;
        }

        private GetOrdersQueryResultItem MaotpGetOrdersQueryResultItem(GetOrdersTransactionResultItem item)
        {
            var result = new GetOrdersQueryResultItem
            {
                Exchange = item.Exchange,
                Id = item.Id,
                PairSymbols = item.PairSymbols,
                Price = item.Price,
                Status = item.Status,
                TotalAmount = item.TotalAmount,
                Type = item.Type,
                Volume = item.Volume
            };

            return result;
        }
    }
}
