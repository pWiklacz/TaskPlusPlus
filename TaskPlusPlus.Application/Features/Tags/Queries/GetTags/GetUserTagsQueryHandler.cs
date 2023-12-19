using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Tag;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Application.Features.Tags.Queries.GetTags;
internal sealed class GetUserTagsQueryHandler : IQueryHandler<GetUserTagsQuery, List<TagDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetUserTagsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<Result<List<TagDto>>> Handle(GetUserTagsQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }
        var userId = userResult.Value.Id;
        var spec = new UserTagsSpecification(userId);
        var tags = await _unitOfWork.Repository<Tag, TagId>().ListAsync(spec);
        return _mapper.Map<List<TagDto>>(tags);
    }
}
