using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages.Events
{
    public class PageMovedDomainEvent : DomainEventBase
    {
        public Guid? ParentPageId { get; }

        public PageMovedDomainEvent(Guid? parentPageId)
        {
            ParentPageId = parentPageId;
        }
    }
}