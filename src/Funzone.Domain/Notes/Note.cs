using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.Notes
{
    public class Note : Entity, IAggregateRoot
    {
        public NoteId Id { get; private set; }
        private ZoneId _zoneId;
        private UserId _userId;
        private DateTime _createdTime;
        private string _title;
        private string _content;

        public Note(
            ZoneId zoneId, 
            UserId userId,
            string title, 
            string content)
        {
            Id = new NoteId(Guid.NewGuid());
            _createdTime = SystemClock.Now;

            _zoneId = zoneId;
            _userId = userId;
            _title = title;
            _content = content;
        }
    }
}