using TaskPlusPlus.Application.DTOs;
using TaskPlusPlus.Application.Messaging;


namespace TaskPlusPlus.Application.Features.Categories.Commands.UpdateCategorySettings;

public sealed record UpdateCategorySettingsCommand(UpdateCategorySettingsDto Dto) : ICommand;

