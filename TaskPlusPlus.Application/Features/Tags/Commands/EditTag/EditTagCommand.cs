using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Commands.EditTag;

public sealed record EditTagCommand(UpdateTagDto Dto) : ICommand;
