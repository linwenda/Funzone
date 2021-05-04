using System;
using Funzone.Domain.SeedWork;

namespace Funzone.Domain.TodoLists
{
    public class TodoListId : TypedIdValueBase
    {
        public TodoListId(Guid value) : base(value)
        {
        }
    }
}