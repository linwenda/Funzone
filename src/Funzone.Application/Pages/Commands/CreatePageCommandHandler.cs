using System;
using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Domain.Pages;
using Funzone.Domain.SeedWork;
using Funzone.Domain.Users;
using Funzone.Domain.Zones;

namespace Funzone.Application.Pages.Commands
{
    public class CreatePageCommandHandler : ICommandHandler<CreatePageCommand, Guid>
    {
        private readonly IAggregateStore _aggregateStore;
        private readonly IUserContext _userContext;

        public CreatePageCommandHandler(IAggregateStore aggregateStore,IUserContext userContext)
        {
            _aggregateStore = aggregateStore;
            _userContext = userContext;
        }
        
        public Task<Guid> Handle(CreatePageCommand request, CancellationToken cancellationToken)
        {
            var page = Page.Create(
                new ZoneId(request.ZoneId),
                request.ParentId,
                _userContext.UserId,
                request.Title,
                request.Body);

            _aggregateStore.AppendChanges(page);

            return Task.FromResult(page.Id);
        }
    }
}