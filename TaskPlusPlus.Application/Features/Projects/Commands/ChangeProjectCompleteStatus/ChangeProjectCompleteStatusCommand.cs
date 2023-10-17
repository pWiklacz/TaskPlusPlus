using System.Windows.Input;
using TaskPlusPlus.Application.DTOs.Common;
using ICommand = TaskPlusPlus.Application.Messaging.ICommand;

namespace TaskPlusPlus.Application.Features.Projects.Commands.ChangeProjectCompleteStatus;

public sealed record ChangeProjectCompleteStatusCommand(ChangeCompleteStatusDto Dto) : ICommand; 
