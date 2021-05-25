using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Pages;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using MediatR;

namespace Funzone.Application.Pages.Commands
{
    public class SavePageDraftCommandHandler : ICommandHandler<SavePageDraftCommand, Unit>
    {
        private readonly IAggregateStore _aggregateStore;
        private readonly IUserContext _userContext;

        public SavePageDraftCommandHandler(IAggregateStore aggregateStore,IUserContext userContext)
        {
            _aggregateStore = aggregateStore;
            _userContext = userContext;
        }
        
        public async Task<Unit> Handle(SavePageDraftCommand request, CancellationToken cancellationToken)
        {
            var page = await _aggregateStore.Load(new PageId(request.PageId));

            page.SaveDraft(_userContext.UserId, request.Title, request.Body);
            
            _aggregateStore.AppendChanges(page);

            return Unit.Value;
        }
    }
}