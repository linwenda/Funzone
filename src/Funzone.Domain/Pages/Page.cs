using System;
using Funzone.Domain.Pages.Templates;
using Funzone.Domain.SeedWork;
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
        private PageType _type;
        private bool _deleted;

        private Page()
        {
            //Only for EF
        }

        private Page(PageId parentId, UserId authorId, string title, string body, PageType type)
        {
            _parentId = parentId;
            _authorId = authorId;
            _title = title;
            _body = body;
            _type = type;
        }

        public static Page CreateUntitled(PageId parentId, UserId authorId, string title, string body)
        {
            return new Page(parentId, authorId, title, body, PageType.Untitled);
        }
    }
}