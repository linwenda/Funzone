using System;

namespace Funzone.Domain.SeedWork
{
    public abstract class AggregateId<T> where T : AggregateRoot
    {
        public Guid Value { get; }

        protected AggregateId(Guid value)
        {
            Value = value;
        }
    }
}