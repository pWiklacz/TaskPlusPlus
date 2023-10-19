using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Application.Messaging;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.ChangeTaskEnergy;

public record ChangeTaskEnergyCommand(ChangeTaskEnergyDto Dto) : ICommand;
