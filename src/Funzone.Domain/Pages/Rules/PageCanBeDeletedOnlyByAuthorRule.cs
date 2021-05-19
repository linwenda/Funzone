using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Rules
{
    public class PageCanBeDeletedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _deleteUserId;

        public PageCanBeDeletedOnlyByAuthorRule(UserId authorId, UserId deleteUserId)
        {
            _authorId = authorId;
            _deleteUserId = deleteUserId;
        }
        public bool IsBroken()
        {
            return _authorId != _deleteUserId;
        }

        public string Message => "Only author can delete.";
    }
}