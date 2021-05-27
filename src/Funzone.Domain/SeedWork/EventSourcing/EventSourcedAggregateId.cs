using System;

namespace Funzone.Domain.SeedWork.EventSourcing
{
    public abstract class EventSourcedAggregateId<T> where T : EventSourcedAggregateRoot
    {
        public Guid Value { get; }

        protected EventSourcedAggregateId(Guid value)
        {
            Value = value;
        }
    }
}