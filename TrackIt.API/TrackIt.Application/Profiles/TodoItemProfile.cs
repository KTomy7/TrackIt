using AutoMapper;
using TrackIt.Application.DTOs;
using TrackIt.Domain.Entities;
using TrackIt.Domain.Enums;

namespace TrackIt.Application.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemEntity, TodoItemDto>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (int)src.Priority)); ;

            CreateMap<TodoItemCreateDto, TodoItemEntity>()
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => (PriorityEnum)src.Priority));
        }
    }
}
