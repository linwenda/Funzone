using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Zones.Events
{
    public class ZoneDeletedDomainEvent : DomainEventBase
    {
        public ZoneId ZoneId { get; }

        public ZoneDeletedDomainEvent(ZoneId zoneId)
        {
            ZoneId = zoneId;
        }
    }
}