using AutoMapper;
using TaskPlusPlus.Application.DTOs.Project;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Profiles.Projects;

public class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Notes,
                opt => opt.MapFrom(src => src.Notes.Value))
            .ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.DueDate! != null! ? src.DueDate.Value : (DateOnly?)null))
            .ForMember(dest => dest.DueTime,
                opt => opt.MapFrom(src => src.DueTime))
            .ForMember(dest => dest.IsCompleted,
                opt => opt.MapFrom(src => src.IsCompleted))
            .ForMember(dest => dest.Tasks,
                opt => opt.MapFrom(src => src.Tasks))
            .ForMember(dest => dest.CompletedOnUtc,
                opt => opt.MapFrom(src => src.CompletedOnUtc));
    }
}