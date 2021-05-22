using System.Collections.Generic;
using System.Threading.Tasks;

namespace Funzone.Domain.SeedWork
{
    public interface IAggregateStore
    {
        Task Save();

        Task<T> Load<T>(AggregateId<T> aggregateId)
            where T : AggregateRoot;

        List<IDomainEvent> GetChanges();

        void AppendChanges<T>(T aggregate)
            where T : AggregateRoot;

        void ClearChanges();
    }
}