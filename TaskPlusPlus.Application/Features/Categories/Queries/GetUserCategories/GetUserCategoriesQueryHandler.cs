using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Category;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Application.Features.Categories.Queries.GetUserCategories;
internal sealed class GetUserCategoriesQueryHandler : IQueryHandler<GetUserCategoriesQuery, List<CategoryDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext; 

    public GetUserCategoriesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<List<CategoryDto>>> Handle(GetUserCategoriesQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;
        var spec = new CategoriesByUserIdSpecification(userId);
        var categories = await _unitOfWork.Repository<Category, CategoryId>().ListAsync(spec);
        return _mapper.Map<List<CategoryDto>>(categories);
    }
}
