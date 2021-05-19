using System;
using Funzone.Domain.Pages.Events;
using Funzone.Domain.Pages.Rules;
using Funzone.Domain.Pages.Templates;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Newtonsoft.Json;

namespace Funzone.Domain.Pages
{
    public partial class Page : Entity, IAggregateRoot
    {
        public PageId Id { get; private set; }

        private PageId _parentId;
        private UserId _authorId;
        private DateTime _createdTime;
        private string _title;
        private string _body;
        private PageType _type;
        private bool _isDeleted;

        private Page()
        {
            //Only for EF
        }

        private Page(
            PageId parentId,
            UserId authorId, 
            string title, 
            IPageTemplate body, 
            PageType type)
        {
            _parentId = parentId;
            _authorId = authorId;
            _title = title;
            _body = JsonConvert.SerializeObject(body);
            _type = type;
            _createdTime = SystemClock.Now;

            AddDomainEvent(new PageCreatedDomainEvent(Id, 
                _parentId, 
                _authorId, 
                _title, 
                _type,
                _createdTime));
        }

        public static Page CreateZone(UserId authorId, string title)
        {
            return new Page(null, authorId, title, new Zone(), PageType.Zone);
        }

        public Page CreateArticle(UserId creatorId, string title, Article article)
        {
            CheckRule(new ArticleCanBeCreatedOnlyByAuthorRule(_authorId, creatorId));
            return new Page(Id, _authorId, title, article, PageType.Article);
        }
        
        public void Delete(UserId deleteUserId)
        {
            CheckRule(new PageCanBeDeletedOnlyByAuthorRule(_authorId, deleteUserId));
            _isDeleted = true;
        }
    }
}