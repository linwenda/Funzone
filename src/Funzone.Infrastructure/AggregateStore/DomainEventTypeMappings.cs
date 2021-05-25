using System;
using System.Collections.Generic;
using Funzone.Domain.Pages.Events;

namespace Funzone.Infrastructure.AggregateStore
{
    internal static class DomainEventTypeMappings
    {
        internal static IDictionary<string, Type> Dictionary { get; }

        static DomainEventTypeMappings()
        {
            Dictionary = new Dictionary<string, Type>
            {
                {"PageCreated", typeof(PageCreatedDomainEvent)},
                {"PageDraftSaved", typeof(PageDraftSavedDomainEvent)},
                {"PageMoved", typeof(PageMovedDomainEvent)}
            };
        }
    }
}