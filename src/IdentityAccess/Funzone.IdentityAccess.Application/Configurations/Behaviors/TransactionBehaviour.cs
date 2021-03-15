﻿using Funzone.BuildingBlocks.Application.Commands;
using Funzone.BuildingBlocks.Infrastructure;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Funzone.IdentityAccess.Application.Configurations.Behaviors
{
    public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionBehaviour(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            await _unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }
}