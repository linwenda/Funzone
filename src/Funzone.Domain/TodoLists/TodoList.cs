using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Todos;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.TodoLists
{
    public class TodoList : Entity, IAggregateRoot
    {
        public TodoListId Id { get; private set; }
        private ZoneId _zoneId;
        private UserId _userId;
        private DateTime _createdTime;
        private string _title;

        private TodoList()
        {
            //Only for EF
        }

        public TodoList(ZoneId zoneId, UserId userId, string title)
        {
            Id = new TodoListId(Guid.NewGuid());
            _createdTime = SystemClock.Now;

            _zoneId = zoneId;
            _userId = userId;
            _title = title;
        }

        public void Rename(string title)
        {
            _title = title;
        }

        public Todo AddTodo(UserId userId, string title, string content)
        {
            return new Todo(Id, userId, title, content);
        }
    }
}