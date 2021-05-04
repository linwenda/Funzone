using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.Todos
{
    public class TodoId : TypedIdValueBase
    {
        public TodoId(Guid value) : base(value)
        {
        }
    }
}