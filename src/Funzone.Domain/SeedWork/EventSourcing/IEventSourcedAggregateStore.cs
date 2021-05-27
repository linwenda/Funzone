using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funzone.Domain.SeedWork.EventSourcing
{
    public interface IEventSourcedAggregateStore
    {
        Task Save();

        Task<T> Load<T>(EventSourcedAggregateId<T> aggregateId)
            where T : EventSourcedAggregateRoot;

        List<IDomainEvent> GetChanges();

        void AppendChanges<T>(T aggregate)
            where T : EventSourcedAggregateRoot;

        void ClearChanges();
    }
}