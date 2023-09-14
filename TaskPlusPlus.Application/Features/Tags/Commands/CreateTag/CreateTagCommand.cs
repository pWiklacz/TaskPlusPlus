using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Commands.CreateTag;

public sealed record CreateTagCommand(CreateTagDto Dto) : ICommand;
