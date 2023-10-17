using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Projects.Commands.UpdateProjectNameAndNotes;

public record UpdateProjectNameAndNotesCommand(UpdateNameAndNotesDto Dto) : ICommand;

