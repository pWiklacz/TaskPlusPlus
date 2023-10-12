using AutoMapper;
using TaskPlusPlus.Application.DTOs.Tag;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Profiles.Tags;

public class TagMappingProfile : Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.ColorHex,
                opt => opt.MapFrom(src => src.ColorHex.Value))
            .ForMember(dest => dest.IsFavorite,
                opt => opt.MapFrom(src => src.IsFavorite))
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.Value));
    }
}