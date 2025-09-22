using Microsoft.EntityFrameworkCore;
using TrackIt.Domain.Entities;
using TrackIt.Infrastructure.DataAccess;

namespace TrackIt.Infrastructure.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TrackItDbContext _dbContext;

        public TodoItemRepository(TrackItDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<TodoItemEntity>> GetPendingTodoItemsAsync()
        {
            return await _dbContext.TodoItems
                .Where(x => !x.IsDone)
                .OrderByDescending(x => x.Priority)
                .ThenBy(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task CreateTodoItemAsync(TodoItemEntity todoItemEntity)
        {
            _dbContext.Add(todoItemEntity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> MarkAsDone(int id)
        {
            var todoItem = await _dbContext.TodoItems.FindAsync(id);
            if (todoItem == null || todoItem.IsDone)
            {
                return false;
            }

            todoItem.IsDone = true;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
