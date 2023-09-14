using FluentResults;
using MediatR;

namespace TaskPlusPlus.Application.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}