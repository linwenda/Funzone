using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageDraftSavedDomainEvent : DomainEventBase
    {
        public string Title { get; }
        public string Body { get; }
        public PageStatus Status { get; }

        public PageDraftSavedDomainEvent(string title, string body, PageStatus status)
        {
            Title = title;
            Body = body;
            Status = status;
        }
    }
}