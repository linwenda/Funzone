using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Rules
{
    public class ArticleCanBeCreatedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _creatorId;

        public ArticleCanBeCreatedOnlyByAuthorRule(UserId authorId, UserId creatorId)
        {
            _authorId = authorId;
            _creatorId = creatorId;
        }


        public bool IsBroken()
        {
            return _authorId != _creatorId;
        }

        public string Message => "Only author can create.";
    }
}