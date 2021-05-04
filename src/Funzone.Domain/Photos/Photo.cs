using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Domain.Photos
{
    public class Photo : Entity, IAggregateRoot
    {
        public PhotoId Id { get; private set; }
        private ZoneId _zoneId;
        private UserId _userId;
        private string _title;
        private DateTime _createdTime;
        private string _link;

        private Photo()
        {
            //Only for EF
        }

        public Photo(
            ZoneId zoneId,
            UserId authorId,
            string title,
            string link)
        {
            Id = new PhotoId(Guid.NewGuid());
            _createdTime = SystemClock.Now;

            _zoneId = zoneId;
            _userId = authorId;
            _title = title;
            _link = link;
        }
    }
}