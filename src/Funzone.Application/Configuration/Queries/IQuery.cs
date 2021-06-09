﻿using Funzone.Application.Configuration.Responses;
using MediatR;

namespace Funzone.Application.Configuration.Queries
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}