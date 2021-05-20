using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Events
{
    public class ZoneCreatedDomainEvent : DomainEventBase
    {
        public ZoneId ZoneId { get; }
        public UserId CreatorId { get; }
        public string Title { get; }

        public ZoneCreatedDomainEvent(
            ZoneId zoneId,
            UserId creatorId,
            string title)
        {
            ZoneId = zoneId;
            CreatorId = creatorId;
            Title = title;
        }
    }
}