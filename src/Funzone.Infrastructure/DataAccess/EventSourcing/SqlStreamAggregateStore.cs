using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Data;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SeedWork.EventSourcing;
using Funzone.Infrastructure.Serialization;
using Newtonsoft.Json;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace Funzone.Infrastructure.DataAccess.EventSourcing
{
    public class SqlStreamAggregateStore : IEventSourcedAggregateStore
    {
        private readonly IStreamStore _streamStore;

        private readonly List<IDomainEvent> _appendedChanges;

        private readonly List<AggregateToSave> _aggregatesToSave;

        public SqlStreamAggregateStore(
            ISqlConnectionFactory sqlConnectionFactory)
        {
            _appendedChanges = new List<IDomainEvent>();

            _streamStore = new MsSqlStreamStoreV3(
                new MsSqlStreamStoreV3Settings(sqlConnectionFactory.GetConnectionString()));

            _aggregatesToSave = new List<AggregateToSave>();
        }

        public async Task Save()
        {
            foreach (var aggregateToSave in _aggregatesToSave)
            {
                await _streamStore.AppendToStream(
                    GetStreamId(aggregateToSave.Aggregate),
                    aggregateToSave.Aggregate.Version,
                    aggregateToSave.Messages.ToArray());
            }

            _aggregatesToSave.Clear();
        }

        public async Task<T> Load<T>(EventSourcedAggregateId<T> aggregateId)
            where T : EventSourcedAggregateRoot
        {
            var streamId = GetStreamId(aggregateId);

            IList<IDomainEvent> domainEvents = new List<IDomainEvent>();
            ReadStreamPage readStreamPage;
            int position = StreamVersion.Start;
            int take = 100;
            do
            {
                readStreamPage = await _streamStore.ReadStreamForwards(streamId, position, take);
                var messages = readStreamPage.Messages;
                foreach (var streamMessage in messages)
                {
                    Type type = DomainEventTypeMappings.Dictionary[streamMessage.Type];
                    var jsonData = await streamMessage.GetJsonData();
                    var domainEvent = JsonConvert.DeserializeObject(jsonData, type) as IDomainEvent;

                    domainEvents.Add(domainEvent);
                }

                position += take;
            }
            while (!readStreamPage.IsEnd);

            if (!domainEvents.Any())
            {
                return null;
            }

            var aggregate = (T)Activator.CreateInstance(typeof(T), true);

            aggregate.Load(domainEvents);

            return aggregate;
        }

        public List<IDomainEvent> GetChanges()
        {
            return _appendedChanges;
        }

        public void AppendChanges<T>(T aggregate)
            where T : EventSourcedAggregateRoot
        {
            _aggregatesToSave.Add(new AggregateToSave(aggregate, CreateStreamMessages(aggregate).ToList()));
        }

        public void ClearChanges()
        {
            _appendedChanges.Clear();
        }

        private class AggregateToSave
        {
            public AggregateToSave(EventSourcedAggregateRoot aggregate, List<NewStreamMessage> messages)
            {
                Aggregate = aggregate;
                Messages = messages;
            }

            public EventSourcedAggregateRoot Aggregate { get; }

            public List<NewStreamMessage> Messages { get; }
        }

        private NewStreamMessage[] CreateStreamMessages<T>(
            T aggregate)
            where T : EventSourcedAggregateRoot
        {
            List<NewStreamMessage> newStreamMessages = new List<NewStreamMessage>();

            var domainEvents = aggregate.GetDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                var jsonData = JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings
                {
                    ContractResolver = new AllPropertiesContractResolver()
                });
                var message = new NewStreamMessage(
                    domainEvent.Id,
                    MapDomainEventToType(domainEvent),
                    jsonData);
                newStreamMessages.Add(message);
                _appendedChanges.Add(domainEvent);
            }

            return newStreamMessages.ToArray();
        }

        private string MapDomainEventToType(IDomainEvent domainEvent)
        {
            foreach (var key in DomainEventTypeMappings.Dictionary.Keys)
            {
                if (DomainEventTypeMappings.Dictionary[key] == domainEvent.GetType())
                {
                    return key;
                }
            }

            throw new ArgumentException("Invalid Domain Event type", nameof(domainEvent));
        }

        private static string GetStreamId<T>(T aggregate)
            where T : EventSourcedAggregateRoot
        {
            return $"{aggregate.GetType().Name}-{aggregate.Id:N}";
        }

        private static string GetStreamId<T>(EventSourcedAggregateId<T> aggregateId)
            where T : EventSourcedAggregateRoot
            => $"{typeof(T).Name}-{aggregateId.Value:N}";
    }
}