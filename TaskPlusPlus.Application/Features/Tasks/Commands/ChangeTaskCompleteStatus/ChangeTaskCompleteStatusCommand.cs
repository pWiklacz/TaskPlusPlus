using TaskPlusPlus.Application.DTOs.Common;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskCompleteStatus;
public record ChangeTaskCompleteStatusCommand(ChangeCompleteStatusDto Dto) : ICommand; 
