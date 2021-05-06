using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Zones.Rules
{
    public class ZoneCanBeEditedOnlyByCreatorRule : IBusinessRule
    {
        private readonly UserId _creatorId;
        private readonly UserId _editorId;

        public ZoneCanBeEditedOnlyByCreatorRule(UserId creatorId, UserId editorId)
        {
            _creatorId = creatorId;
            _editorId = editorId;
        }

        public bool IsBroken()
        {
            return _creatorId != _editorId;
        }

        public string Message => "Only the creator can edit it";
    }
}