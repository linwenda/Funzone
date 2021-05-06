using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeDeletedOnlyByCreatorRule : IBusinessRule
    {
        private readonly UserId _creatorId;
        private readonly UserId _deletedUserId;

        public ZoneCanBeDeletedOnlyByCreatorRule(UserId creatorId, UserId deletedUserId)
        {
            _creatorId = creatorId;
            _deletedUserId = deletedUserId;
        }

        public bool IsBroken()
        {
            return _creatorId != _deletedUserId;
        }

        public string Message => "Only the creator can delete it";
    }
}