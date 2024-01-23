using AutoMapper;
using TaskPlusPlus.Application.DTOs.Task;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Profiles.Tasks;
public class CalendarTaskMappingProfile : Profile
{
    public CalendarTaskMappingProfile()
    {
        CreateMap<Task, CalendarTaskDto>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.DueDate! != null! ? src.DueDate.Value : (DateOnly?)null))
            .ForMember(dest => dest.IsCompleted,
                opt => opt.MapFrom(src => src.IsCompleted))
            .ForMember(dest => dest.CategoryId,
                opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(dest => dest.ProjectId,
                opt => opt.MapFrom(src => src.ProjectId != null ? src.ProjectId.Value : (ulong?)null));
    }
}
