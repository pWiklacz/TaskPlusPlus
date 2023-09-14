using FluentResults;
using MediatR;

namespace TaskPlusPlus.Application.Messaging;

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>
{
}