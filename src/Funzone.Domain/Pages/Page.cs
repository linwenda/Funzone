using System;
using Funzone.Domain.Pages.Events;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SeedWork.EventSourcing;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages
{
    public class Page : EventSourcedAggregateRoot
    {
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private string _body;
        private PageStatus _status;

        private Page()
        {
        }

        public static Page Create(UserId authorId, string title, string body)
        {
            var page = new Page();

            var pageCreatedDomainEvent = new PageCreatedDomainEvent(
                Guid.NewGuid(),
                authorId.Value,
                DateTime.UtcNow,
                title,
                body,
                PageStatus.Draft.Value);

            page.Apply(pageCreatedDomainEvent);
            page.AddDomainEvent(pageCreatedDomainEvent);

            return page;
        }

        public void Edit(UserId editorId, string title, string body)
        {
            if (_authorId != editorId)
            {
                throw new PageDomainException("Only author can edit it.");
            }

            var pageEditedDomainEvent = new PageEditedDomainEvent(
                title, 
                body, 
                PageStatus.Draft.Value);

            Apply(pageEditedDomainEvent);
            AddDomainEvent(pageEditedDomainEvent);
        }

        public void Close(UserId closedUserId)
        {
            if (_authorId != closedUserId)
            {
                throw new PageDomainException("Only author can close it.");
            }

            var pageClosedDomainEvent = new PageClosedDomainEvent(PageStatus.Closed.Value);

            Apply(pageClosedDomainEvent);
            AddDomainEvent(pageClosedDomainEvent);
        }

        protected override void Apply(IDomainEvent @event)
        {
            this.When((dynamic) @event);
        }

        private void When(PageCreatedDomainEvent @event)
        {
            Id = @event.PageId;

            _authorId = new UserId(@event.AuthorId);
            _createdTime = @event.CreatedTime;
            _title = @event.Title;
            _body = @event.Body;
            _status = PageStatus.Of(@event.Status);
        }

        private void When(PageEditedDomainEvent @event)
        {
            _title = @event.Title;
            _body = @event.Body;
            _status = PageStatus.Of(@event.Status);
        }

        private void When(PageClosedDomainEvent @event)
        {
            _status = PageStatus.Of(@event.Status);
        }
    }
}