using System;
using System.Collections.Generic;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate.Events
{
    public class PageCreatedDomainEvent : DomainEventBase
    {
        public Guid PageId { get; }
        public Guid AuthorId { get; }
        public DateTime CreatedTime { get; }
        public string Title { get; }
        public List<Block> Blocks { get; }

        public PageCreatedDomainEvent(
            Guid pageId,
            Guid authorId,
            DateTime createdTime,
            string title,
            List<Block> blocks)
        {
            PageId = pageId;
            AuthorId = authorId;
            CreatedTime = createdTime;
            Title = title;
            Blocks = blocks;
        }
    }
}