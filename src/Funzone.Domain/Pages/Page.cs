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
        private Guid? _parentId;
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private string _body;
        private DateTime? _editedTime;
        private bool _isDeleted;

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
                DateTime.Now);

            page.Apply(pageCreatedDomainEvent);
            page.AddDomainEvent(pageCreatedDomainEvent);

            return page;
        }

        public void Delete(UserId deleteUserId)
        {
            CheckRule(new PageCanBeDeletedOnlyByAuthorRule(_authorId, deleteUserId));
            
            var pageDeletedDomainEvent = new PageDeletedDomainEvent(Id);
            
            Apply(pageDeletedDomainEvent);
            AddDomainEvent(pageDeletedDomainEvent);
        }

        public void Edit(UserId editorId, string title, string body)
        {
            CheckRule(new PageCanBeEditedOnlyByAuthorRule(_authorId, editorId));

            var pageEditedDomainEvent = new PageEditedDomainEvent(title, body, SystemClock.Now);

            Apply(pageEditedDomainEvent);
            AddDomainEvent(pageEditedDomainEvent);
        }
        
        private void When(PageCreatedDomainEvent @event)
        {
            Id = @event.PageId;
            _parentId = @event.ParentPageId;
            _title = @event.Title;
            _body = @event.Body;
            _authorId = @event.AuthorId;
            _createdTime = @event.CreatedTime;
        }

        private void When(PageDeletedDomainEvent @event)
        {
            _isDeleted = true;
        }

        private void When(PageEditedDomainEvent @event)
        {
            _title = @event.Title;
            _body = @event.Body;
            _editedTime = @event.EditedTime;
        }

        private void When(PageMovedDomainEvent @event)
        {
            _parentId = @event.ParentPageId;
        }
    }
}