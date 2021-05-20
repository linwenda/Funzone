using Funzone.Domain.SeedWork;      
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeModifiedOnlyByCreatorRule : IBusinessRule
    {
        private readonly UserId _creatorId;
        private readonly UserId _currentUserId;

        public ZoneCanBeModifiedOnlyByCreatorRule(UserId creatorId, UserId currentUserId)
        {
            _creatorId = creatorId;
            _currentUserId = currentUserId;
        }

        public bool IsBroken() => _creatorId != _currentUserId;

        public string Message => "Only the creator of a zone can modify it.";
    }
}