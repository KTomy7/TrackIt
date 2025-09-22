using TrackIt.Application.DTOs;

namespace TrackIt.Application.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetPendingTodoItemsAsync();
        Task CreateTodoItemAsync(TodoItemCreateDto dto);
        Task<bool> MarkAsDone(int id);
    }
}
