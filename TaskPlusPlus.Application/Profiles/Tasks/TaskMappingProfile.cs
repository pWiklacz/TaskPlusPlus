using AutoMapper;
using TaskPlusPlus.Application.DTOs.Task;
using TaskPlusPlus.Domain.Entities;
using Task = TaskPlusPlus.Domain.Entities.Task;

namespace TaskPlusPlus.Application.Profiles.Tasks;

public class TaskMappingProfile : Profile
{
    public TaskMappingProfile()
    {
        CreateMap<Task, TaskDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.Notes,
                opt => opt.MapFrom(src => src.Notes.Value))
            .ForMember(dest => dest.DueDate,
                opt => opt.MapFrom(src => src.DueDate != null ? src.DueDate.Value : (DateTime?)null))
            .ForMember(dest => dest.Priority,
                opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.ProjectId,
                opt => opt.MapFrom(src => src.ProjectId != null ? src.ProjectId.Value : (ulong?)null))
            .ForMember(dest => dest.Energy,
                opt => opt.MapFrom(src => src.Energy))
            .ForMember(dest => dest.IsCompleted,
                opt => opt.MapFrom(src => src.IsCompleted))
            .ForMember(dest => dest.DurationTime,
                opt => opt.MapFrom(src => src.DurationTime))
            .ForMember(dest => dest.UserId,
                opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.SubTasks,
                opt => opt.MapFrom(src => src.SubTasks))
            .ForMember(dest => dest.Tags,
                opt => opt.MapFrom(src => src.Tags));

        //CreateMap<TaskDto, Task>()
        //    .ConstructUsing(dto => Task.Create(dto.Id, dto.Name, dto.DueDate, dto.Notes, dto.Priority, dto.ProjectId,
        //            dto.Energy, dto.DurationTime, dto.UserId).Value)
        //    .ForMember(dest => dest.SubTasks,
        //        opt => opt.MapFrom(src => src.SubTasks))
        //    .ForMember(dest => dest.Tags,
        //        opt => opt.MapFrom(src => src.Tags)); 
    }
}