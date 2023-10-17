using AutoMapper;
using FluentResults;
using TaskPlusPlus.Application.Contracts.Persistence;
using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Application.Messaging;
using TaskPlusPlus.Application.Models.Identity.ApplicationUser;
using TaskPlusPlus.Application.Specifications.Project;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Project;

namespace TaskPlusPlus.Application.Features.Projects.Queries.GetUserProjects;
internal sealed class GetUserProjectsQueryHandler : IQueryHandler<GetUserProjectsQuery, List<ProjectDto>>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _userContext;

    public GetUserProjectsQueryHandler(IMapper mapper, IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<Result<List<ProjectDto>>> Handle(GetUserProjectsQuery request, CancellationToken cancellationToken)
    {
        var userResult = _userContext.GetCurrentUser();
        if (userResult.IsFailed)
        {
            return userResult.ToResult();
        }

        var userId = userResult.Value.Id;
        var spec = new UserProjectsWithTasksSpecification(userId);
        var projects = await _unitOfWork.Repository<Project, ProjectId>().ListAsync(spec);

        return _mapper.Map<List<ProjectDto>>(projects);
    }
}
