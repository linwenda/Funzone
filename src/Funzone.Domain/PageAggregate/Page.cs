using System;
using System.Collections.Generic;
using System.Linq;
using Funzone.Domain.PageAggregate.Events;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Newtonsoft.Json;

namespace Funzone.Domain.PageAggregate
{
    public class Page : AggregateRoot
    {
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private PageStatus _status;
        private List<Block> _blocks;

        private Page()
        {
            _blocks = new List<Block>();
        }

        public static Page Create(UserId authorId, string title, List<Block> blocks)
        {
            var page = new Page();

            var pageCreatedDomainEvent = new PageCreatedDomainEvent(
                Guid.NewGuid(),
                authorId.Value,
                DateTime.UtcNow,
                title,
                blocks);

            page.Apply(pageCreatedDomainEvent);
            page.AddDomainEvent(pageCreatedDomainEvent);

            return page;
        }

        public void Edit(
            UserId editorId,
            string title, 
            List<Block> editedBlocks)
        {
            CheckAuthor(editorId);

            var pageEditedDomainEvent = new PageEditedDomainEvent(
                title,
                editedBlocks);

            Apply(pageEditedDomainEvent);
            AddDomainEvent(pageEditedDomainEvent);
        }

        public void Publish(UserId publishedId)
        {
            CheckAuthor(publishedId);

            var pagePublishedDomainEvent = new PagePublishedDomainEvent();

            Apply(pagePublishedDomainEvent);
            AddDomainEvent(pagePublishedDomainEvent);
        }

        public void Archive(UserId closedUserId)
        {
            CheckAuthor(closedUserId);

            var pageClosedDomainEvent = new PageArchivedDomainEvent();

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
            _blocks = @event.Blocks;
            _status = PageStatus.Draft;
        }

        private void When(PageEditedDomainEvent @event)
        {
            _title = @event.Title;
            _status = PageStatus.Draft;

            foreach (var editedBlock in @event.Blocks)
            {
                var block = _blocks.FirstOrDefault(b => b.Id == editedBlock.Id);
                if (block != null)
                {
                    block.Edit(editedBlock.Text, editedBlock.IsRemoved);
                }
                else
                {
                    _blocks.Add(editedBlock);
                }
            }
        }

        private void When(PagePublishedDomainEvent @event)
        {
            _status = PageStatus.Published;
        }

        private void When(PageArchivedDomainEvent @event)
        {
            _status = PageStatus.Archived;
        }

        private void CheckAuthor(UserId currentUserId)
        {
            if (_authorId != currentUserId)
            {
                throw new PageDomainException("Only author can operate it");
            }
        }
    }
}