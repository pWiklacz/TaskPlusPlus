using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Queries.GetTagById;
public record GetTagByIdQuery(ulong Id) : IQuery<TagDto>;