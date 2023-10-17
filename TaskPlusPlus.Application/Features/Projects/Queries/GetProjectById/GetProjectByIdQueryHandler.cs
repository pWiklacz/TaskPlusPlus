using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Project;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.Errors;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Queries.GetProjectById;
internal sealed class GetProjectByIdQueryHandler : IQueryHandler<GetProjectByIdQuery, ProjectDto>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetProjectByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<Result<ProjectDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;
        var spec = new UserProjectsWithTasksSpecification(request.Id, userId);

        var project = await _unitOfWork.Repository<Project, ProjectId>().GetEntityWithSpec(spec);
        if (project is null)
        {
            return Result.Fail<ProjectDto>(new NotFoundError(
                nameof(Project), request.Id));
        }
        return _mapper.Map<ProjectDto>(project);
    }
}
