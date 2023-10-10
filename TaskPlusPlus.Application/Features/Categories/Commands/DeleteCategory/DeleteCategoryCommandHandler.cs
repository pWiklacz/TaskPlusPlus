﻿using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Commands.DeleteCategory;
internal sealed class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{

    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<Category, CategoryId>().GetByIdAsync(request.Id);

        if (category is null)
        {
            return Result.Fail(new NotFoundError(nameof(Category), request.Id));
        }

        _unitOfWork.Repository<Category, CategoryId>().Remove(category);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new DeleteSuccess(nameof(Category)));
    }
}
