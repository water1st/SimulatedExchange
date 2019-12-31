using Dapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SimulatedExchange.DataAccess.Mapper;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.ReportingTransaction.Orders
{
    public partial class OrderReporting :
        IReportingTransactionHandler<AddOrderTransaction>,
        IReportingTransactionHandler<UpdateOrderStatusTransaction>,
        IReportingTransactionHandler<UpdateOrderTransaction>,
        IReportingTransactionHandler<GetOrdersTransaction, GetOrdersTransactionResult>,
        IReportingTransactionHandler<GetOrderTransaction, GetOrderTransactionResult>
    {
        private readonly IDbConnectionFactory factory;
        private readonly IOrderMapper mapper;
        private readonly ILogger logger;

        public OrderReporting(IDbConnectionFactory factory, IOrderMapper mapper, ILogger<OrderReporting> logger)
        {
            this.factory = factory;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GetOrdersTransactionResult> Handle(GetOrdersTransaction request, CancellationToken cancellationToken)
        {
            var connection = factory.Create(ConnectionType.Reporting);

            const string SELECT_SQL = "SELECT * FROM orders ORDER BY CreatedTimeUtc DESC ";
            const string PAGING_SQL = " LIMIT @offset,@take ";
            const string COUNT_SQL = "SELECT COUNT(1) / @pageSize AS PageCount FROM orders";
            string selectSQL = SELECT_SQL;
            var offset = 0;
            var take = 0;
            int pageIndex = 1;
            int totalPageCount = 1;
            if (request.PagingOptions != null
                && request.PagingOptions.PageIndex > 0
                && request.PagingOptions.PageSize > 0)
            {
                selectSQL += PAGING_SQL;
                offset = (request.PagingOptions.PageIndex - 1) * request.PagingOptions.PageSize;
                take = request.PagingOptions.PageSize;
                var count = await connection.QuerySingleAsync<double>(COUNT_SQL, new { pageSize = request.PagingOptions.PageSize });
                pageIndex = request.PagingOptions.PageIndex;
                totalPageCount = Convert.ToInt32(Math.Ceiling(count));
            }
            var queryResults = await connection.QueryAsync<PersistentObject>(selectSQL, new
            {
                offset,
                take
            });

            var result = new GetOrdersTransactionResult();

            foreach (var data in queryResults)
            {
                var item = mapper.MapToGetOrdersTransactionResultItem(data);
                result.Add(item);
            }
            result.PagingInfo = new PagingInfo { CurrentPageIndex = pageIndex, PageCount = totalPageCount };
            return result;
        }

        public async Task<Unit> Handle(AddOrderTransaction request, CancellationToken cancellationToken)
        {
            var symbols = request.Symbols.Split('-');
            const string INSERT_SQL = "INSERT INTO orders (Id,ClientId,FromCurrencySymbol,ToCurrencySymbol,Price,Volume,TotalAmount,Type,Status,Exchange,CreatedTimeUtc) VALUES (@Id,@ClientId,@FromCurrencySymbol,@ToCurrencySymbol,@Price,@Volume,@TotalAmount,@Type,@Status,@Exchange,@CreatedTimeUtc)";
            await ExecuteSQLAsync(INSERT_SQL, new PersistentObject
            {
                Id = request.Id,
                FromCurrencySymbol = symbols[1],
                ToCurrencySymbol = symbols[0],
                Price = request.Price,
                Volume = 0,
                TotalAmount = request.Amount,
                Exchange = request.Exchange,
                Type = request.Type,
                Status = 0,
                ClientId = request.ClientId,
                CreatedTimeUtc = request.DateTime
            });

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateOrderStatusTransaction request, CancellationToken cancellationToken)
        {
            const string UPDATE_SQL = "UPDATE orders SET Status = @Status,ModifyedTimeUtc = @ModifyedTimeUtc WHERE Id = @Id";

            await ExecuteSQLAsync(UPDATE_SQL, new
            {
                Id = request.Id.ToString(),
                Status = request.Status,
                ModifyedTimeUtc = request.DateTime
            });

            return Unit.Value;
        }

        public async Task<Unit> Handle(UpdateOrderTransaction request, CancellationToken cancellationToken)
        {
            const string UPDATE_SQL = "UPDATE orders SET Volume = @Volume,Status = @Status,ModifyedTimeUtc = @ModifyedTimeUtc WHERE Id = @Id";

            await ExecuteSQLAsync(UPDATE_SQL, new
            {
                Id = request.Id.ToString(),
                Status = request.Status,
                Volume = request.Volume,
                ModifyedTimeUtc = request.DateTime
            });

            return Unit.Value;
        }

        public async Task<GetOrderTransactionResult> Handle(GetOrderTransaction request, CancellationToken cancellationToken)
        {
            const string SELECT_SQL = "SELECT * FROM orders WHERE Id = @id";

            var connection = factory.Create(ConnectionType.Reporting);
            var data = await connection.QueryFirstOrDefaultAsync<PersistentObject>(SELECT_SQL, new { id = request.Id.ToString() });
            var result = mapper.MapToGetOrderTransactionResult(data);

            return result;
        }

        private async Task ExecuteSQLAsync(string sql, object @object)
        {
            using (var connection = factory.Create(ConnectionType.Reporting))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        await connection.ExecuteAsync(sql, @object, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, ex.Message);
                        transaction.Rollback();
                    }
                }
                connection.Close();
            }

        }
    }
}
