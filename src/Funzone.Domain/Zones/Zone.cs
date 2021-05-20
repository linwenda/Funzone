using System;
using Funzone.Domain.SeedWork;
using Funzone.Domain.SharedKernel;
using Funzone.Domain.Users;
using Funzone.Domain.Zones.Events;
using Funzone.Domain.Zones.Rules;

namespace Funzone.Domain.Zones
{
    public class Zone : Entity, IAggregateRoot
    {
        public ZoneId Id { get; private set; }
        private DateTime _createdTime;
        private UserId _creatorId;
        private string _title;
        private Visibility _visibility;
        private string _color;
        private string _icon;
        private bool _isDeleted;

        private Zone()
        {
            //Only for EF
        }

        public Zone(
            UserId creatorId,
            string title,
            Visibility visibility,
            string color,
            string icon)
        {
            Id = new ZoneId(Guid.NewGuid());
            _createdTime = SystemClock.Now;
            _creatorId = creatorId;
            _title = title;
            _visibility = visibility;
            _color = color;
            _icon = icon;

            AddDomainEvent(new ZoneCreatedDomainEvent(Id, _creatorId, _title));
        }

        public void Edit(UserId currentUserId,
            string title, 
            Visibility visibility,
            string color, 
            string icon)
        {
            CheckRule(new ZoneCanBeModifiedOnlyByCreatorRule(_creatorId, currentUserId));
            _title = title;
            _visibility = visibility;
            _color = color;
            _icon = icon;
        }

        public void Delete(UserId currentUserId)
        {
            CheckRule(new ZoneCanBeModifiedOnlyByCreatorRule(_creatorId, currentUserId));
            _isDeleted = true;
        }
    }
}