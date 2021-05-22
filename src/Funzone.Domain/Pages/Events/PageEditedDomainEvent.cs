using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageEditedDomainEvent : DomainEventBase
    {
        public string Title { get; }
        public string Body { get; }
        public DateTime EditedTime { get; }

        public PageEditedDomainEvent(string title, string body, DateTime editedTime)
        {
            Title = title;
            Body = body;
            EditedTime = editedTime;
        }
    }
}