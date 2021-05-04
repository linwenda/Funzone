using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.TodoLists;
using Funzone.Domain.Users;
using Funzone.Domain.Zones.Rules;

namespace Funzone.Domain.Zones
{
    public class Zone : Entity, IAggregateRoot
    {
        public ZoneId Id { get; private set; }

        private ZoneId _parentId;
        private UserId _userId;
        private ZoneType _type;
        private string _name;
        private DateTime _createdTime;

        private Zone()
        {
        }

        public Zone(
            ZoneId parentId,
            ZoneType type,
            UserId userId,
            string title)
        {
            Id = new ZoneId(Guid.NewGuid());
            _createdTime = SystemClock.Now;

            _parentId = parentId;
            _userId = userId;
            _type = type;
            _name = title;
        }

        public void Rename(string title)
        {
            _name = title;
        }

        public TodoList AddTodoList(UserId userId,string title)
        {
            CheckRule(new ZoneOnlyBoardTypeCanAddedTodoListRule(_type));

            return new TodoList(Id, userId, title);
        }
    }
}