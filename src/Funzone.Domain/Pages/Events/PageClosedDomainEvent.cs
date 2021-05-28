using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageClosedDomainEvent : DomainEventBase
    {
        public string Status { get; }

        public PageClosedDomainEvent(string status)
        {
            Status = status;
        }
    }
}