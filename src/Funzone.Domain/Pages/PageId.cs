using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Pages
{
    public class PageId : TypedIdValueBase
    {
        public PageId(Guid value) : base(value)
        {
        }
    }
}