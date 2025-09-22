using AutoMapper;
using TrackIt.Application.DTOs;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.Repositories;

namespace TrackIt.Application.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;
        private readonly IMapper _mapper;

        public TodoItemService(ITodoItemRepository todoItemRepository, IMapper mapper)
        {
            _todoItemRepository = todoItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TodoItemDto>> GetPendingTodoItemsAsync()
        {
            var todoItems = await _todoItemRepository.GetPendingTodoItemsAsync();
            return _mapper.Map<IEnumerable<TodoItemDto>>(todoItems);
        }

        public async Task CreateTodoItemAsync(TodoItemCreateDto dto)
        {
            var todoItem = _mapper.Map<TodoItemEntity>(dto);
            todoItem.IsDone = false;

            await _todoItemRepository.CreateTodoItemAsync(todoItem);
        }

        public async Task<bool> MarkAsDone(int id)
        {
            return await _todoItemRepository.MarkAsDone(id);
        }
    }
}
