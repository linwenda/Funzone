using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageEditedDomainEvent : DomainEventBase
    {
        public string Title { get; }
        public string Body { get; }
        public string Status { get; }

        public PageEditedDomainEvent(
            string title,
            string body,
            string status)
        {
            Title = title;
            Body = body;
            Status = status;
        }
    }
}