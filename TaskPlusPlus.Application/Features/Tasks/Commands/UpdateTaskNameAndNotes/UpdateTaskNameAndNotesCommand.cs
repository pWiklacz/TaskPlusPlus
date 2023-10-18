using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.UpdateTaskNameAndNotes;
public record UpdateTaskNameAndNotesCommand(UpdateNameAndNotesDto Dto) : ICommand;
