using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.Contracts.Persistence.Repositories;
using TaskPlusPlus.Application.DTOs.Task.Validators;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Responses.Errors;
using TaskPlusPlus.Application.Responses.Successes;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Features.Tasks.Commands.CreateTask;

internal sealed class CreateTaskCommandHandler : ICommandHandler<CreateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly  IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public CreateTaskCommandHandler(ITaskRepository taskRepository, 
        IUnitOfWork unitOfWork, 
        ICategoryRepository categoryRepository, 
        IProjectRepository projectRepository, 
        ITagRepository tagRepository, IUserContext userContext)
    {
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
        _projectRepository = projectRepository;
        _tagRepository = tagRepository;
        _userContext = userContext;
    }

    public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {

        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;

        var dto = request.Dto;
        var validator = new CreateTaskDtoValidator(
            _categoryRepository, 
            _projectRepository,
            _tagRepository);

        var validationResult = await validator.ValidateAsync(dto, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Fail(new ValidationError(validationResult, nameof(Task)));
        }
        
        //TODO:: UserID
        var result = Task.Create(dto.Name, dto.DueDate, dto.Notes, dto.Priority, dto.ProjectId, 
            dto.Energy, dto.DurationTime, userId, dto.CategoryId);

        if (result.IsFailed)
            return result.ToResult();

        var task = result.Value;

        _taskRepository.Add(task);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok()
            .WithSuccess(new CreationSuccess(nameof(Task)));
    }
}