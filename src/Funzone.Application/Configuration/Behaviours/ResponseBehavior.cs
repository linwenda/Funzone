using System.Threading;
using System.Threading.Tasks;
using Funzone.Application.Configuration.Exceptions;
using Funzone.Application.Configuration.Responses;
using Funzone.Domain.SeedWork;
using MediatR;

namespace Funzone.Application.Configuration.Behaviours
{
    public class ResponseBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : IResponse
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (NotFoundException ex)
            {
                var parameters = new object[] {ex.Message};
                
                return (TResponse) typeof(TResponse).GetMethod("Invalid")?.Invoke(null, parameters);
            }
            catch (ValidationException ex)
            {
                var parameters = new object[] {ex.Errors};
                
                return (TResponse) typeof(TResponse).GetMethod("Invalid")?.Invoke(null, parameters);
            }
            catch (DomainException ex)
            {
                var parameters = new object[] {ex.Code, new string[] {ex.Message}};

                return (TResponse) typeof(TResponse).GetMethod("Error")?.Invoke(null, parameters);
            }
        }
    }
}