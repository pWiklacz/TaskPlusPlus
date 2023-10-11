using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetCategories;
internal sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetCategoriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.Repository<Category, CategoryId>().ListAllAsync();
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
