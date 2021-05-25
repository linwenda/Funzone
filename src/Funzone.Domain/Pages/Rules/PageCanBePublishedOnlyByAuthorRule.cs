using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Rules
{
    public class PageCanBePublishedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _userId;

        public PageCanBePublishedOnlyByAuthorRule(UserId authorId,UserId userId)
        {
            _authorId = authorId;
            _userId = userId;
        }
        
        public bool IsBroken()
        {
            return _authorId != _userId;
        }

        public string Message => "Only author can publish page";
    }
}