using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimulatedExchange.Domain;
using SimulatedExchange.EventSourcing;
using System;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.EventSourcing
{
    internal partial class MementoStorage : IMementoStorage
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ILogger logger;

        public MementoStorage(IDbConnectionFactory connectionFactory, ILogger<MementoStorage> logger)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
        }

        public async Task<BaseMemento> GetMementoAsync(Guid aggregateId)
        {
            const string SELECT_SQL = "SELECT * FROM memento_storage WHERE Version = (SELECT MAX(Version) FROM memento_storage WHERE AggregateId = @aggregateId) AND AggregateId = @aggregateId";

            var result = await GetMementoAsync(SELECT_SQL, new { aggregateId = aggregateId.ToString() });
            return result;
        }

        public async Task<BaseMemento> GetMementoAsync(Guid aggregateId, int maxVersion)
        {
            const string SELECT_SQL = "SELECT * FROM memento_storage WHERE Version <= @version AND AggregateId = @aggregateId";

            var result = await GetMementoAsync(SELECT_SQL, new { aggregateId = aggregateId.ToString(), version = maxVersion });
            return result;
        }

        private async Task<BaseMemento> GetMementoAsync(string sql, object parm)
        {
            var connection = connectionFactory.Create(ConnectionType.EventSourcing);

            var data = await connection.QueryFirstOrDefaultAsync<PersistentObject>(sql, parm);

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

            using (var connection = connectionFactory.Create(ConnectionType.EventSourcing))
            {
                connection.Open();
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
                connection.Close();
            }


        }
    }
}
