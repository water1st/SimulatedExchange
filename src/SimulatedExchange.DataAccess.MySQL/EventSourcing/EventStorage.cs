﻿using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SimulatedExchange.Domain;
using SimulatedExchange.Events;
using SimulatedExchange.EventSourcing;
using SimulatedExchange.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SimulatedExchange.DataAccess.EventSourcing
{
    internal partial class EventStorage : IEventStorage
    {
        private readonly IDbConnectionFactory connectionFactory;
        private readonly ILogger<EventStorage> logger;
        private readonly IMementoStorage mementoStorage;
        private readonly Assembly assembly;
        public EventStorage(IDbConnectionFactory connectionFactory,
            ILogger<EventStorage> logger, IMementoStorage mementoStorage)
        {
            this.connectionFactory = connectionFactory;
            this.logger = logger;
            this.mementoStorage = mementoStorage;
            assembly = Assembly.Load("SimulatedExchange.Domain");
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId)
        {
            const string SELECT_SQL = "SELECT * FROM events_storage WHERE AggregateId = @aggregateId ORDER BY Version ASC";

            var result = await GetEventsAsync(SELECT_SQL, new { aggregateId = aggregateId.ToString() });

            if (!result.Any())
            {
                throw new AggregateNotFoundException($"找不到聚合根：\"{aggregateId}\"");
            }

            return result;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId, int maxVersion)
        {
            const string SELECT_SQL = "SELECT * FROM events_storage WHERE AggregateId = @aggregateId AND Version <= @version ORDER BY Version ASC";

            var result = await GetEventsAsync(SELECT_SQL, new { aggregateId = aggregateId.ToString(), version = maxVersion });

            if (!result.Any())
            {
                throw new AggregateNotFoundException($"找不到聚合根：\"{aggregateId}\"");
            }

            return result;
        }

        private async Task<IEnumerable<Event>> GetEventsAsync(string sql, object parm)
        {
            var connection = connectionFactory.Create(ConnectionType.EventSourcing);
            var datas = await connection.QueryAsync<PersistentObject>(sql, parm);
            var result = datas.Select(data =>
            {
                var json = data.Event;
                var type = assembly.GetType(data.EventType);
                var @event = JsonConvert.DeserializeObject(json, type);
                return (Event)@event;
            });

            return result;
        }

        public async Task SaveEventsAsync<TAggregateRoot>(TAggregateRoot aggregateRoot) where TAggregateRoot : AggregateRoot
        {
            var events = aggregateRoot.UncommittedEvent;
            var version = aggregateRoot.Version;

            const string INSERT_SQL = "INSERT INTO events_storage (Id,AggregateId,Event,EventType,Version) VALUES (@id,@aggregateId,@event,@eventType,@version)";
            using (var connection = connectionFactory.Create(ConnectionType.EventSourcing))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var @event in events)
                        {
                            version++;
                            @event.Version = version;
                            //每1024个事件创建一个快照
                            if (version % 1024 == 0)
                            {
                                var memento = aggregateRoot.GetMemento();
                                memento.Version = version;
                                await mementoStorage.SaveMementoAsync(memento);
                            }
                            var json = JsonConvert.SerializeObject(@event);
                            var type = @event.GetType();

                            await connection.ExecuteAsync(INSERT_SQL, new
                            {
                                Id = Guid.NewGuid().ToString(),
                                aggregateId = aggregateRoot.Id.ToString(),
                                Event = json,
                                EventType = type.FullName,
                                Version = version
                            }, transaction);
                        }

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
