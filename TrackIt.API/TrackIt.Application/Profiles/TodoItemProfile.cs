using AutoMapper;
using TrackIt.Application.DTOs;
using TrackIt.Domain.Entities;

namespace TrackIt.Application.Profiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItemEntity, TodoItemDto>();

            CreateMap<CreateTodoItemDto, TodoItemEntity>();
        }
    }
}
