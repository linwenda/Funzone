using Funzone.Domain.SeedWork;
using Funzone.Domain.TodoLists;
using Funzone.Domain.Users;

namespace Funzone.Domain.Todos
{
    public class Todo : Entity, IAggregateRoot
    {
        public TodoId Id { get; private set; }

        private TodoListId _listId;
        private UserId _userId;
        private string _title;
        private string _content;

        private Todo()
        {
            //Only for EF
        }

        public Todo(
            TodoListId listId, 
            UserId userId, 
            string title, 
            string content)
        {
            _listId = listId;
            _userId = userId;
            _title = title;
            _content = content;
        }
    }
}