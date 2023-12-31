﻿using FluentValidation;
using TaskPlusPlus.Application.DTOs.Common.IDto;

namespace TaskPlusPlus.Application.DTOs.Common.Validators;
internal class DueDateDtoValidator : AbstractValidator<IDueDateDto>
{
    public DueDateDtoValidator()
    {
        RuleFor(dto => dto.DueDate)
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now)).WithMessage("Due date cannot be in the past");
    }
}
