﻿using FluentResults;
using MediatR;

namespace TaskPlusPlus.Application.Messaging;

public interface IQueryHandler<TQuery, TResponse>
: IRequestHandler<TQuery, Result<TResponse>>
where TQuery : IQuery<TResponse>
{
}