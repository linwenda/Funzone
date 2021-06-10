using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageAggregate.Events
{
    public class PageEditedDomainEvent : DomainEventBase
    {
        public string Title { get; }
        public string Blocks { get; }

        public PageEditedDomainEvent(
            string title,
            string blocks)
        {
            Title = title;
            Blocks = blocks;
        }
    }
}