using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeCreatedNewOnlyByCreatorRule : IBusinessRule
    {
        private readonly UserId _creatorId;
        private readonly UserId _userId;

        public ZoneCanBeCreatedNewOnlyByCreatorRule(UserId creatorId, UserId userId)
        {
            _creatorId = creatorId;
            _userId = userId;
        }

        public bool IsBroken()
        {
            return _creatorId != _userId;
        }

        public string Message => "Only the creator can create children zone it";
    }
}