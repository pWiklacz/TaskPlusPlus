using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Application.Features.Tags.Commands.DeleteTag;

internal sealed class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteTagCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Repository<Tag, TagId>().GetByIdAsync(request.Id);

        if (tag is null)
        {
            return  Result.Fail(new NotFoundError(nameof(Tag), request.Id));
        }

        _unitOfWork.Repository<Tag, TagId>().Remove(tag);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new DeleteSuccess(nameof(Tag)));
    }
}