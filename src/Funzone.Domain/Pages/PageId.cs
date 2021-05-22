using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages
{
    public class PageId : AggregateId<Page>
    {
        public PageId(Guid value) : base(value)
        {
        }
    }
}