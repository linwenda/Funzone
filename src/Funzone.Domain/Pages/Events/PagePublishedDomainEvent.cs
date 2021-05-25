using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PagePublishedDomainEvent : DomainEventBase
    {
        public PageStatus Status { get; }

        public PagePublishedDomainEvent(PageStatus status)
        {
            Status = status;
        }
    }
}