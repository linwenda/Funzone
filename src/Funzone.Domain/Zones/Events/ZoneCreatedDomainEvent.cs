using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Events
{
    public class ZoneCreatedDomainEvent : DomainEventBase
    {
        public ZoneId ZoneId { get; }
        public ZoneId ParentZoneId { get; }
        public ZoneType Type { get; }
        public UserId CreatorId { get; }
        public string Name { get; }
        public string Description { get; }
        public string Icon { get; }

        public ZoneCreatedDomainEvent(
            ZoneId zoneId,
            ZoneId parentZoneId,
            ZoneType type,
            UserId creatorId,
            string name,
            string description,
            string icon)
        {
            ZoneId = zoneId;
            ParentZoneId = parentZoneId;
            Type = type;
            CreatorId = creatorId;
            Name = name;
            Description = description;
            Icon = icon;
        }
    }
}