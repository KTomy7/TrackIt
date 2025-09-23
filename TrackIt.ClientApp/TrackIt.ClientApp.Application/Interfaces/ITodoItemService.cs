using TrackIt.ClientApp.Domain.Entities;

namespace TrackIt.ClientApp.Application.Interfaces
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> GetPendingTodoItemsAsync();
        Task CreateTodoItemAsync(CreateTodoItem todoItem);
        Task MarkAsDoneAsync(int id);
    }
}
