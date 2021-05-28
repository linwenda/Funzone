using System;
using System.Collections.Generic;

namespace Funzone.Infrastructure.DataAccess.EventSourcing
{
   internal static class DomainEventTypeMappings
    {
        internal static IDictionary<string, Type> Dictionary { get; }

        static DomainEventTypeMappings()
        {
            Dictionary = new Dictionary<string, Type>();
        }
    }
}