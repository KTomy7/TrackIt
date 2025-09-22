using TrackIt.Domain.Entities;

namespace TrackIt.Infrastructure.Repositories
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<TodoItemEntity>> GetPendingTodoItemsAsync();
        Task CreateTodoItemAsync(TodoItemEntity todoItemEntity);
        Task<bool> MarkAsDone(int id);
    }
}
