using AutoMapper;
using TaskPlusPlus.Application.DTOs.Category;
using TaskPlusPlus.Domain.Entities;

namespace TaskPlusPlus.Application.Profiles.Categories;
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name.Value))
            .ForMember(dest => dest.IsFavorite,
                opt => opt.MapFrom(src => src.IsFavorite))
            .ForMember(dest => dest.ColorHex,
                opt => opt.MapFrom(src => src.ColorHex.Value))
            .ForMember(dest => dest.Icon,
                opt => opt.MapFrom(src => src.Icon));
    }
}
