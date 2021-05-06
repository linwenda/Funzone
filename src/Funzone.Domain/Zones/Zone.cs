using System;
using Funzone.Domain.Notes;
using Funzone.Domain.Photos;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.TodoLists;
using Funzone.Domain.Users;
using Funzone.Domain.Zones.Events;
using Funzone.Domain.Zones.Rules;

namespace Funzone.Domain.Zones
{
    public class Zone : Entity, IAggregateRoot
    {
        public ZoneId Id { get; private set; }

        private ZoneId _parentId;
        private UserId _creatorId;
        private ZoneType _type;
        private string _name;
        private DateTime _createdTime;
        private string _description;
        private string _icon;
        private bool _deleted;

        private Zone()
        {
        }

        private Zone(
            ZoneId parentId,
            ZoneType type,
            UserId userId,
            string name,
            string description,
            string icon)
        {
            Id = new ZoneId(Guid.NewGuid());
            _createdTime = SystemClock.Now;

            _parentId = parentId;
            _creatorId = userId;
            _type = type;
            _name = name;
            _description = description;
            _icon = icon;

            AddDomainEvent(new ZoneCreatedDomainEvent(
                Id,
                _parentId,
                _type,
                _creatorId,
                _name,
                _description,
                _icon));
        }

        public static Zone Create(
            UserId userId,
            ZoneType type,
            string name,
            string description,
            string icon)
        {
            return new Zone(null, type, userId, name, description, icon);
        }
      
        public Zone CreateChildren(
            UserId userId,
            ZoneType type,
            string name,
            string description,
            string icon)
        {
            CheckRule(new ZoneCanBeCreatedNewOnlyByCreatorRule(_creatorId, userId));
            return new Zone(Id, type, _creatorId, name, description, icon);
        }

        public void Rename(string title)
        {
            _name = title;
        }

        public void Edit(
            UserId editorId,
            string name,
            string description,
            string icon)
        {
            CheckRule(new ZoneCanBeEditedOnlyByCreatorRule(_creatorId, editorId));
            _name = name;
            _description = description;
            _icon = icon;
        }

        public void Delete(UserId deletedUserId)
        {
            CheckRule(new ZoneCanBeDeletedOnlyByCreatorRule(_creatorId, deletedUserId));
            _deleted = true;

            AddDomainEvent(new ZoneDeletedDomainEvent(Id));
        }

        public Note AddNote(
            string title,
            string content)
        {
            CheckRule(new NoteCanOnlyBeAddedToNotebookZoneRule(_type));
            return new Note(Id, _creatorId, title, content);
        }

        public Photo AddPhoto(
            string title,
            string link)
        {
            CheckRule(new PhotoCanOnlyBeAddedToPhotoAlbumZoneRule(_type));
            return new Photo(Id, _creatorId, title, link);
        }

        public TodoList AddTodoList(
            UserId userId,
            string title)
        {
            CheckRule(new TodoListCanOnlyBeAddedToBoardZoneRule(_type));
            return new TodoList(Id, userId, title);
        }
    }
}