using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageDeletedDomainEvent : DomainEventBase
    {
        public Guid PageId { get; }

        public PageDeletedDomainEvent(Guid pageId)
        {
            PageId = pageId;
        }
    }
}