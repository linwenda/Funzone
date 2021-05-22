using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Pages;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using MediatR;

namespace Funzone.Application.Pages.Commands
{
    public class DeletePageCommandHandler : ICommandHandler<DeletePageCommand, Unit>
    {
        private readonly IAggregateStore _aggregateStore;
        private readonly IUserContext _userContext;

        public DeletePageCommandHandler(IAggregateStore aggregateStore, IUserContext userContext)
        {
            _aggregateStore = aggregateStore;
            _userContext = userContext;
        }

        public async Task<Unit> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var page = await _aggregateStore.Load(new PageId(request.PageId));

            page.Delete(_userContext.UserId);

            _aggregateStore.AppendChanges(page);

            return Unit.Value;
        }
    }
}