using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate.Events
{
    public class PageCreatedDomainEvent : DomainEventBase
    {
        public Guid PageId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedTime { get; }
        public string Title { get; }
        public string Body { get; }

        public PageCreatedDomainEvent(
            Guid pageId,
            Guid authorId,
            DateTime createdTime,
            string title,
            string body)
        {
            PageId = pageId;
            AuthorId = authorId;
            CreatedTime = createdTime;
            Title = title;
            Body = body;
        }
    }
}