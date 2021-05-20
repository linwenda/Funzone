using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Events
{
    public class PageCreatedDomainEvent : DomainEventBase
    {
        public PageCreatedDomainEvent(
            PageId pageId,
            PageId parentPageId,
            UserId authorId,
            string title,
            DateTime createdTime)
        {
            PageId = pageId;
            AuthorId = authorId;
            Title = title;
            CreatedTime = createdTime;
            ParentPageId = parentPageId;
        }

        public PageId PageId { get; }
        public PageId ParentPageId { get; }
        public UserId AuthorId { get; }
        public string Title { get; }
        public DateTime CreatedTime { get; }
    }
}