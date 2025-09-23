using TrackIt.Application.DTOs;

namespace TrackIt.Application.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItemDto>> GetPendingTodoItemsAsync();
        Task CreateTodoItemAsync(CreateTodoItemDto dto);
        Task<bool> MarkAsDone(int id);
    }
}
