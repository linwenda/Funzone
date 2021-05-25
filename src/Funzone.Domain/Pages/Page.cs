using System;
using Funzone.Domain.Pages.Events;
using Funzone.Domain.Pages.Rules;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.Pages
{
    public class Page : AggregateRoot
    {
        private ZoneId _zoneId;
        private PageId _parentId;
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private string _body;
        private PageStatus _status;
        private DateTime? _editedTime;

        private Page()
        {
            //Only for EF
        }

        protected override void Apply(IDomainEvent @event)
        {
            When((dynamic) @event);
        }

        public static Page Create(
            ZoneId zoneId,
            Guid? parentId,
            UserId authorId,
            string title,
            string body)
        {
            var page = new Page();

            var pageCreatedDomainEvent = new PageCreatedDomainEvent(
                zoneId,
                Guid.NewGuid(),
                parentId,
                authorId,
                title,
                body,
                DateTime.Now,
                PageStatus.Draft);

            page.Apply(pageCreatedDomainEvent);
            page.AddDomainEvent(pageCreatedDomainEvent);

            return page;
        }

        public void SaveDraft(UserId userId, string title, string body)
        {
            CheckRule(new PageDraftCanBeSavedOnlyByAuthorRule(_authorId, userId));

            var pageDraftSavedDomain = new PageDraftSavedDomainEvent(title, body, PageStatus.Draft);

            Apply(pageDraftSavedDomain);
            AddDomainEvent(pageDraftSavedDomain);
        }

        public void Publish(UserId userId)
        {
            CheckRule(new PageCanBePublishedOnlyByAuthorRule(_authorId, userId));

            _status = PageStatus.Published;
            
            var pagePublishedDomainEvent = new PagePublishedDomainEvent(_status);

            Apply(pagePublishedDomainEvent);
            AddDomainEvent(pagePublishedDomainEvent);
        }

        private void When(PageCreatedDomainEvent @event)
        {
            Id = @event.PageId;
            _parentId = @event.ParentPageId.HasValue ? new PageId(@event.ParentPageId.Value) : null;
            _title = @event.Title;
            _body = @event.Body;
            _authorId = @event.AuthorId;
            _createdTime = @event.CreatedTime;
            _status = @event.Status;
        }

        private void When(PageDraftSavedDomainEvent @event)
        {
            _title = @event.Title;
            _body = @event.Body;
            _status = @event.Status;
        }
        
        private void When(PageMovedDomainEvent @event)
        {
            _parentId = @event.ParentPageId.HasValue ? new PageId(@event.ParentPageId.Value) : null;
        }
    }
}