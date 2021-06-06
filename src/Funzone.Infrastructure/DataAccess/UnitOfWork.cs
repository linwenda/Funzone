using System.Threading;
using System.Threading.Tasks;
using Funzone.Domain.SeedWork;
using Funzone.Infrastructure.Processing;

namespace Funzone.Infrastructure.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDomainEventsDispatcher _domainEventsDispatcher;
        private readonly FunzoneDbContext _dbContext;

        public UnitOfWork(
            IDomainEventsDispatcher domainEventsDispatcher,
            FunzoneDbContext dbContext)
        {
            _domainEventsDispatcher = domainEventsDispatcher;
            _dbContext = dbContext;
        }
        
        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            await _domainEventsDispatcher.DispatchEventsAsync();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}