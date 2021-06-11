using System.Collections.Generic;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate.Events
{
    public class PageEditedDomainEvent : DomainEventBase
    {
        public string Title { get; }
        public List<Block> Blocks { get; }

        public PageEditedDomainEvent(
            string title,
            List<Block> blocks)
        {
            Title = title;
            Blocks = blocks;
        }
    }
}