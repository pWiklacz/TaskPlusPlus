using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetCategoryById;
internal sealed class GetCategoryByIdQueryHandler : IQueryHandler<GetCategoryByIdQuery,CategoryDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoryByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.Repository<Category, CategoryId>().GetByIdAsync(request.Id);
        if (category is null)
        {
            return Result.Fail<CategoryDto>(new NotFoundError(
                nameof(Category), request.Id));
        }

        return _mapper.Map<CategoryDto>(category);
    }
}
