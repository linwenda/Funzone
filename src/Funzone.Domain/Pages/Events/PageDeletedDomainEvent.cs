using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Events
{
    public class PageDeletedDomainEvent : DomainEventBase
    {
        public PageId PageId { get; }
        public UserId AuthorId { get; }

        public PageDeletedDomainEvent(
            PageId pageId, UserId authorId)
        {
            PageId = pageId;
            AuthorId = authorId;
        }
    }
}