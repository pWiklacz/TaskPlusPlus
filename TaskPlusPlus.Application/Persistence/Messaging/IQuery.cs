using FluentResults;
using MediatR;

namespace TaskPlusPlus.Application.Persistence.Messaging;

public interface IQuery<TResponse>
    : IRequest<Result<TResponse>>
{
}