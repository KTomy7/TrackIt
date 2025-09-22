using Microsoft.AspNetCore.Mvc;
using TrackIt.Application.DTOs;
using TrackIt.Application.Services;

namespace TrackIt.API.Controllers
{
    /// <summary>
    /// Controller for managing todo items.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemController"/> class.
        /// </summary>
        /// <param name="todoItemService">Service for managing todo items.</param>
        public TodoItemController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        /// <summary>
        /// Gets the list of pending (not done) todo items, sorted by priority and creation date.
        /// </summary>
        /// <returns>A list of pending todo items, or HTTP 204 No Content if none found.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetPendingTodoItems()
        {
            var todoItems = await _todoItemService.GetPendingTodoItemsAsync();
            if (todoItems == null || !todoItems.Any())
            {
                return NoContent();
            }

            return Ok(todoItems);
        }

        /// <summary>
        /// Creates a new todo item.
        /// </summary>
        /// <param name="todoItemCreateDto">The todo item data to create.</param>
        /// <returns>HTTP 200 OK if created successfully.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateTodoItem([FromBody] TodoItemCreateDto todoItemCreateDto)
        {
            await _todoItemService.CreateTodoItemAsync(todoItemCreateDto);
            return Ok();
        }

        /// <summary>
        /// Marks the specified todo item as done.
        /// </summary>
        /// <param name="id">The ID of the todo item to mark as done.</param>
        /// <returns>HTTP 200 OK if successful, or HTTP 404 Not Found if the item does not exist.</returns>
        [HttpPatch("{id}/done")]
        public async Task<IActionResult> MarkAsDone([FromRoute] int id)
        {
            var success = await _todoItemService.MarkAsDone(id);
            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
