﻿using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Tag;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Application.Features.Tags.Queries.GetTagById;
internal sealed class GetTagByIdQueryHandler : IQueryHandler<GetTagByIdQuery, TagDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetTagByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }

    public async Task<Result<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }
        var userId = userResult.Value.Id;
        var spec = new UserTagsSpecification(request.Id, userId);

        var tag = await _unitOfWork.Repository<Tag, TagId>().GetEntityWithSpec(spec);

        if (tag is null)
        {
            return Result.Fail<TagDto>(new NotFoundError(
                nameof(Tag), request.Id));
        }

        return _mapper.Map<TagDto>(tag);
    }
}
