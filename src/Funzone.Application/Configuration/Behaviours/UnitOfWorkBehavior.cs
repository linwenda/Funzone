using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Commands;
using Funzone.Application.Configuration.Extensions;
using Funzone.Domain.SeedWork;
using MediatR;
using Serilog;

namespace Funzone.Application.Configuration.Behaviours
{
    public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public UnitOfWorkBehavior(IUnitOfWork unitOfWork,ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            
            var rowsAffected = await _unitOfWork.CommitAsync(cancellationToken);
            
            var typeName = request.GetGenericTypeName();

            _logger.Information("----- Command {CommandName} committed - rows affected: {@Response}", typeName,
                rowsAffected);

            return response;
        }
    }
}