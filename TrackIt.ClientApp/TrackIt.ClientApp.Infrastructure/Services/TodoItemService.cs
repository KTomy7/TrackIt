using System.Net;
using System.Net.Http.Json;
using TrackIt.ClientApp.Application.Interfaces;
using TrackIt.ClientApp.Domain.Entities;

namespace TrackIt.ClientApp.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly HttpClient _httpClient;

        public TodoItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<TodoItem>> GetPendingTodoItemsAsync()
        {
            var response = await _httpClient.GetAsync("api/TodoItem");
            if (response.StatusCode == HttpStatusCode.NoContent)
                return [];

            response.EnsureSuccessStatusCode();
            var todoItems = await response.Content.ReadFromJsonAsync<List<TodoItem>>();
            return todoItems ?? [];
        }

        public async Task CreateTodoItemAsync(CreateTodoItem todoItem)
        {
            var response = await _httpClient.PostAsJsonAsync("api/TodoItem", todoItem);
            response.EnsureSuccessStatusCode();
        }

        public async Task MarkAsDoneAsync(int id)
        {
            var response = await _httpClient.PatchAsync($"api/TodoItem/{id}/done", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
