using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.Pages.Events
{
    public class PageCreatedDomainEvent : DomainEventBase
    {
        public PageCreatedDomainEvent(
            ZoneId zoneId,
            Guid pageId,
            Guid? parentPageId,
            UserId authorId,
            string title,
            string body,
            DateTime createdTime,
            PageStatus status)
        {
            ZoneId = zoneId;
            PageId = pageId;
            AuthorId = authorId;
            Title = title;
            Body = body;
            CreatedTime = createdTime;
            Status = status;
            ParentPageId = parentPageId;
        }

        public ZoneId ZoneId { get; }
        public Guid PageId { get; }
        public Guid? ParentPageId { get; }
        public UserId AuthorId { get; }
        public string Title { get; }
        public string Body { get; }
        public DateTime CreatedTime { get; }
        public PageStatus Status { get; }
    }
}