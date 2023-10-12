using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Application.Features.Tags.Queries.GetTagById;
internal sealed class GetTagByIdQueryHandler : IQueryHandler<GetTagByIdQuery, TagDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public GetTagByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Repository<Tag, TagId>().GetByIdAsync(request.Id);

        if (tag is null)
        {
            return Result.Fail<TagDto>(new NotFoundError(
                nameof(Tag), request.Id));
        }

        return _mapper.Map<TagDto>(tag);
    }
}
