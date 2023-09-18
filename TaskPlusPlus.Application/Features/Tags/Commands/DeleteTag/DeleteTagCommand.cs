using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tags.Commands.DeleteTag;

public sealed record DeleteTagCommand(ulong Id) : ICommand;
