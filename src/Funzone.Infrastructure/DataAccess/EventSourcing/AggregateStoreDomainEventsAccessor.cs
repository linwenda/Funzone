using System.Collections.Generic;
using System.Linq;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SeedWork.EventSourcing;

namespace Funzone.Infrastructure.DataAccess.EventSourcing
{
    public class AggregateStoreDomainEventsAccessor
    {
        private readonly IEventSourcedAggregateStore _aggregateStore;

        public AggregateStoreDomainEventsAccessor(IEventSourcedAggregateStore aggregateStore)
        {
            _aggregateStore = aggregateStore;
        }

        public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
        {
            return _aggregateStore
                .GetChanges()
                .ToList()
                .AsReadOnly();
        }

        public void ClearAllDomainEvents()
        {
            _aggregateStore.ClearChanges();
        }
    }
}