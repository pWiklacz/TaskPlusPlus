using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Commands.EditTag;

internal sealed class EditTagCommandHandler : ICommandHandler<EditTagCommand>
{
    private readonly ITagRepository _tagRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EditTagCommandHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _unitOfWork = unitOfWork;
    }
    public Task<Result> Handle(EditTagCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
} 