using System.Linq;
using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Infrastructure.DataAccess;
using MediatR;

namespace Funzone.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
    
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly FunzoneDbContext _dbContext;

        public DomainEventsDispatcher(IMediator mediator, FunzoneDbContext dbContext)
        {
            _mediator = mediator;
            _dbContext = dbContext;
        }

        public async Task DispatchEventsAsync()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();
            
            if (domainEntities.Any())
            {
                var domainEvents = domainEntities
                    .SelectMany(x => x.Entity.DomainEvents)
                    .ToList();
                
                domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

                var tasks = domainEvents
                    .Select(async domainEvent =>
                    {
                        await _mediator.Publish(domainEvent);
                    });

                await Task.WhenAll(tasks);
            }
        }
    }
}