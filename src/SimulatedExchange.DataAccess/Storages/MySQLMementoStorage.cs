using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimulatedExchange.DataAccess.Databases;
using SimulatedExchange.Domain;
using SimulatedExchange.Storages;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.Storages
{
    public partial class MySQLMementoStorage : IMementoStorage
    {
        private readonly IDatabaseConnectionFactory connectionFactory;
        private readonly ILogger logger;

        public MySQLMementoStorage(IDatabaseConnectionFactory connectionFactory, ILogger<MySQLMementoStorage> logger)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
        }

        public async Task<BaseMemento> GetMementoAsync(Guid aggregateId)
        {
            const string SELECT_SQL = "SELECT * FROM memento_storage WHERE Version = (SELECT MAX(Version) FROM memento_storage WHERE AggregateId=@aggregateId)";
            var connection = connectionFactory.Create(DatabaseConnectionNames.MYSQL_CONNECTION);

            var data = await connection.QueryFirstOrDefaultAsync<PersistentObject>(SELECT_SQL, new { aggregateId = aggregateId.ToString() });

            if (data == null)
                return null;

            var json = data.Memento;
            var type = Type.GetType(data.MementoType);
            var result = JsonConvert.DeserializeObject(json, type);
            return (BaseMemento)result;
        }

        public async Task SaveMementoAsync(BaseMemento memento)
        {
            const string INSERT_SQL = "INSERT INTO memento_storage (Id,AggregateId,Memento,MementoType,Version) VALUES (@id,@aggregateId,@memento,@mementoType,@version)";

            var json = JsonConvert.SerializeObject(memento);
            var type = memento.GetType();

            var connection = connectionFactory.Create(DatabaseConnectionNames.MYSQL_CONNECTION);

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    await connection.ExecuteAsync(INSERT_SQL, new
                    {
                        id = Guid.NewGuid().ToString(),
                        aggregateId = memento.AggregateRootId.ToString(),
                        memento = json,
                        mementoType = type.FullName,
                        version = memento.Version
                    }, transaction);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, ex.Message);
                    transaction.Rollback();
                }

            }
        }
    }
}

