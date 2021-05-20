using System;
using Funzone.Domain.Pages.Events;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages
{
    public class Page : Entity, IAggregateRoot
    {
        public PageId Id { get; private set; }

        private PageId _parentId;
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private string _body;
        private bool _isDeleted;

        private Page()
        {
            //Only for EF
        }

        public Page(
            PageId parentId,
            UserId authorId, 
            string title, 
            string body)
        {
            _parentId = parentId;
            _authorId = authorId;
            _title = title;
            _body = body;
            _createdTime = SystemClock.Now;

            AddDomainEvent(new PageCreatedDomainEvent(Id, 
                _parentId, 
                _authorId, 
                _title, 
                _createdTime));
        }

        public Page NewPage(UserId currentUserId,string title,string body)
        {
            return new Page(Id, currentUserId, title, body);
        }
    }
}