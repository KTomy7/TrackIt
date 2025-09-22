using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrackIt.Domain.Enums;

namespace TrackIt.Infrastructure.DataAccess.Seed
{
    public static class DbInitializer
    {
        public static async Task SeedDbAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TrackItDbContext>();
            await dbContext.Database.MigrateAsync();

            if (!dbContext.TodoItems.Any())
            {
                dbContext.TodoItems.AddRange(
                    new Domain.Entities.TodoItemEntity
                    {
                        Name = "Buy groceries",
                        Description = "Milk, Bread, Eggs",
                        Priority = PriorityEnum.Medium,
                        CreatedAt = DateTime.UtcNow,
                        IsDone = false
                    },
                    new Domain.Entities.TodoItemEntity
                    {
                        Name = "Finish report",
                        Description = "Complete the quarterly financial report",
                        Priority = PriorityEnum.High,
                        CreatedAt = DateTime.UtcNow,
                        IsDone = false
                    },
                    new Domain.Entities.TodoItemEntity
                    {
                        Name = "Call plumber",
                        Description = "Fix the leaking kitchen sink",
                        Priority = PriorityEnum.Low,
                        CreatedAt = DateTime.UtcNow,
                        IsDone = false
                    }
                );
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
