using System;
using System.Linq;
using Funzone.Domain.SeedWork;

namespace Funzone.UnitTests
{
    public abstract class TestBase
    {
        protected static void ShouldAddedDomainEvent<TDomainEvent>(Entity aggregate)
            where TDomainEvent : IDomainEvent
        {
            var domainEvent = DomainEventsTestHelper
                .GetAllDomainEvents(aggregate)
                .OfType<TDomainEvent>()
                .SingleOrDefault();

            if (domainEvent == null)
            {
                throw new Exception($"{typeof(TDomainEvent).Name} event not added");
            }
        }
    }
}