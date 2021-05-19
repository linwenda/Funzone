using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Pages;
using Funzone.Domain.Pages.Templates;
using Funzone.Domain.Users;

namespace Funzone.Application.Pages.Commands
{
    public class CreateArticleCommandHandler : ICommandHandler<CreateArticleCommand, bool>
    {
        private readonly IUserContext _userContext;
        private readonly IPageRepository _pageRepository;

        public CreateArticleCommandHandler(
            IUserContext userContext,
            IPageRepository pageRepository)
        {
            _userContext = userContext;
            _pageRepository = pageRepository;
        }

        public async Task<bool> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
        {
            var page = await _pageRepository.GetByIdAsync(new PageId(request.PageId));
            
            var subPage = page.CreateArticle(
                _userContext.UserId,
                request.Title,
                new Article {Content = request.Content});

            await _pageRepository.AddAsync(subPage);
            return await _pageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}