using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;

namespace Funzone.Domain.Pages.Rules
{
    public class PageCanBeEditedOnlyByAuthorRule : IBusinessRule
    {
        private readonly UserId _authorId;
        private readonly UserId _editorId;

        public PageCanBeEditedOnlyByAuthorRule(UserId authorId,UserId editorId)
        {
            _authorId = authorId;
            _editorId = editorId;
        }
        
        public bool IsBroken()
        {
            return _authorId != _editorId;
        }

        public string Message => "Only author can edit.";
    }
}