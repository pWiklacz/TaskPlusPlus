using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Commands.UpdateCategorySettings;

internal sealed class UpdateCategorySettingsCommandHandler : ICommandHandler<UpdateCategorySettingsCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategorySettingsCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdateCategorySettingsCommand request, CancellationToken cancellationToken)
    {
        var dto = request.Dto;
        var category = await _unitOfWork.Repository<Category, CategoryId>().GetByIdAsync(dto.Id);

        if (category is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), dto.Id));
        }

        category.UpdateCategorySettings(dto.Settings);

        _unitOfWork.Repository<Category, CategoryId>().Update(category);

        var saveResult = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (saveResult <= 0)
        {
            return Result.Fail(new UpdatingProblemError(nameof(Category)));
        }

        return Result.Ok()
            .WithSuccess(new UpdateSuccess(nameof(Category))); ;
    }
}

