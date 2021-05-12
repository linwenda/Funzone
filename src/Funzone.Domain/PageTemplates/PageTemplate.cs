using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.PageTemplates
{
    public class PageTemplate : IAggregateRoot
    {
        public PageTemplateId Id { get; private set; }

        private string _name;
    }

    public class PageTemplateId : TypedIdValueBase
    {
        public PageTemplateId(Guid value) : base(value)
        {
        }
    }
}