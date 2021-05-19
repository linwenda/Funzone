using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Pages;
using Funzone.Domain.Users;

namespace Funzone.Application.Pages.Commands
{
    public class CreateZoneCommandHandler : ICommandHandler<CreateZoneCommand, bool>
    {
        private readonly IUserContext _userContext;
        private readonly IPageRepository _pageRepository;

        public CreateZoneCommandHandler(IUserContext userContext, IPageRepository pageRepository)
        {
            _userContext = userContext;
            _pageRepository = pageRepository;
        }

        public async Task<bool> Handle(CreateZoneCommand request, CancellationToken cancellationToken)
        {
            var page = Page.CreateZone(_userContext.UserId, request.Title);

            await _pageRepository.AddAsync(page);
            return await _pageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        }
    }
}