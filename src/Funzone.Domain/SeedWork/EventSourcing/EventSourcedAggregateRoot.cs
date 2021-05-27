using System;
using System.Collections.Generic;

namespace Funzone.Domain.SeedWork.EventSourcing
{
    public abstract class EventSourcedAggregateRoot : IEventSourcedAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents;

        public Guid Id { get; protected set; }
        public int Version { get; private set; }

        public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(IDomainEvent @event)
        {
            _domainEvents.Add(@event);
        }

        protected EventSourcedAggregateRoot()
        {
            _domainEvents = new List<IDomainEvent>();

            Version = -1;
        }

        public void Load(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history)
            {
                Apply(e);
                Version++;
            }
        }

        protected abstract void Apply(IDomainEvent @event);
    }
}