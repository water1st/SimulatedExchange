using Dapper;
using Microsoft.Extensions.Logging;
using SimulatedExchange.DataAccess.Databases;
using SimulatedExchange.Domain.Orders;
using SimulatedExchange.Queries.Orders;
using SimulatedExchange.Reporting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.ReportingStorages.Orders
{
    public partial class OrderReportingStorage : IOrderReportingReadOnlyTransactionHandler, IOrderReportingWriteOnlyTransactionHandler
    {
        private readonly IDatabaseConnectionFactory factory;
        private readonly ILogger logger;

        public OrderReportingStorage(IDatabaseConnectionFactory factory, ILogger<OrderReportingStorage> logger)
        {
            this.factory = factory;
            this.logger = logger;
        }

        public async Task<IOrderDetial> Read(GetOrderTransaction readParameter)
        {

            const string SELECT_SQL = "SELECT * FROM orders WHERE Id = @id";

            var connection = factory.Create(DatabaseConnectionNames.MYSQL_READ_DB);
            var data = await connection.QueryFirstOrDefaultAsync<PersistentObject>(SELECT_SQL, new { id = readParameter.Id.ToString() });
            var result = Map(data);

            return result;
        }

        public async Task<IOrderList> Read(GetOrdersTransaction readParameter)
        {
            const string SELECT_SQL = "SELECT * FROM orders ORDER BY CreatedTimeUtc DESC";

            var connection = factory.Create(DatabaseConnectionNames.MYSQL_READ_DB);
            var queryResults = await connection.QueryAsync<PersistentObject>(SELECT_SQL);

            var currentPage = 1;
            var totalPage = 1;
            if (readParameter.Paging != null)
            {
                queryResults = queryResults.Skip((readParameter.Paging.PageIndex - 1) * readParameter.Paging.PageSize).Take(readParameter.Paging.PageSize);
                currentPage = readParameter.Paging.PageIndex;
                totalPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(queryResults.Count()) / Convert.ToDouble(readParameter.Paging.PageSize)));
            }

            var result = new OrderList();
            foreach (var queryResult in queryResults)
            {
                var data = Map(queryResult);
                result.Add(data);
            }
            result.Page = new CurrentPagingInfo(currentPage, totalPage);

            return result;
        }

        public async Task Write(AddOrderTransaction parameter)
        {
            var symbols = parameter.Symbols.Split('-');
            const string INSERT_SQL = "INSERT INTO orders (Id,ClientId,FromCurrencySymbol,ToCurrencySymbol,Price,Volume,TotalAmount,Type,Status,Exchange,CreatedTimeUtc) VALUES (@Id,@ClientId,@FromCurrencySymbol,@ToCurrencySymbol,@Price,@Volume,@TotalAmount,@Type,@Status,@Exchange,@CreatedTimeUtc)";
            await ExecuteSQLAsync(INSERT_SQL, new PersistentObject
            {
                Id = parameter.Id,
                FromCurrencySymbol = symbols[1],
                ToCurrencySymbol = symbols[0],
                Price = parameter.Price,
                Volume = 0,
                TotalAmount = parameter.Amount,
                Exchange = parameter.Exchange,
                Type = parameter.Type,
                Status = 0,
                ClientId = parameter.ClientId,
                CreatedTimeUtc = parameter.DateTime
            });
        }

        public async Task Write(UpdateOrderStatusTransaction parameter)
        {
            const string UPDATE_SQL = "UPDATE orders SET Status = @Status,ModifyedTimeUtc = @ModifyedTimeUtc WHERE Id = @Id";

            await ExecuteSQLAsync(UPDATE_SQL, new
            {
                Id = parameter.Id.ToString(),
                Status = (int)parameter.Status,
                ModifyedTimeUtc = parameter.DateTime
            });
        }

        public async Task Write(UpdateOrderTransaction parameter)
        {
            const string UPDATE_SQL = "UPDATE orders SET Volume = @Volume,Status = @Status,ModifyedTimeUtc = @ModifyedTimeUtc WHERE Id = @Id";

            await ExecuteSQLAsync(UPDATE_SQL, new
            {
                Id = parameter.Id.ToString(),
                Status = (int)parameter.Status,
                Volume = parameter.Volume,
                ModifyedTimeUtc = parameter.DateTime
            });
        }

        private async Task ExecuteSQLAsync(string sql, object @object)
        {
            using (var connection = factory.Create(DatabaseConnectionNames.MYSQL_READ_DB))
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

        private OrderDetial Map(PersistentObject @object)
        {
            var result = new OrderDetial();

            result.Id = @object.Id;
            result.PairSymbols = $"{@object.ToCurrencySymbol}-{@object.FromCurrencySymbol}";
            result.Price = @object.Price;
            result.Status = @object.Status;
            result.Exchange = @object.Exchange;
            result.TotalAmount = @object.TotalAmount;
            result.Type = @object.Type;
            result.Volume = @object.Volume;
            result.ClientId = @object.ClientId;

            return result;
        }
    }
}
