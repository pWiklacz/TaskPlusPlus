using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Queries.GetTags;

public record GetUserTagsQuery() : IQuery<List<TagDto>>;

