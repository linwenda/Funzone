using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageCreatedDomainEvent : DomainEventBase
    {
        public Guid PageId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedTime { get; }
        public string Title { get; }
        public string Body { get; }
        public string Status { get; }

        public PageCreatedDomainEvent(
            Guid pageId,
            Guid authorId,
            DateTime createdTime,
            string title,
            string body,
            string status)
        {
            PageId = pageId;
            AuthorId = authorId;
            CreatedTime = createdTime;
            Title = title;
            Body = body;
            Status = status;
        }
    }
}