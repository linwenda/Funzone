using System.Collections.Generic;
using System.Linq;
using Funzone.Domain.SeedWork;

namespace Funzone.Infrastructure.DataAccess.EventSourcing
{
    public class AggregateStoreDomainEventsAccessor
    {
        private readonly IAggregateStore _aggregateStore;

        public AggregateStoreDomainEventsAccessor(IAggregateStore aggregateStore)
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