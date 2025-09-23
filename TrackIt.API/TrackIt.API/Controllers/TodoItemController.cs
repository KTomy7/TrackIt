using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        private readonly ILogger<TodoItemController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TodoItemController"/> class,
        /// providing endpoints for managing todo items.
        /// </summary>
        /// <param name="todoItemService">The service for managing todo items.</param>
        /// <param name="logger">The logger instance for logging errors and information.</param>

        public TodoItemController(ITodoItemService todoItemService, ILogger<TodoItemController> logger)
        {
            _todoItemService = todoItemService;
            _logger = logger;
        }

        /// <summary>
        /// Gets the list of pending (not done) todo items, sorted by priority and creation date.
        /// Returns HTTP 204 No Content if none found, or HTTP 500 with error details if an exception occurs.
        /// </summary>
        /// <returns>A list of pending todo items, or an appropriate HTTP status code.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetPendingTodoItems()
        {
            try
            {
                var todoItems = await _todoItemService.GetPendingTodoItemsAsync();
                if (todoItems == null || !todoItems.Any())
                {
                    return NoContent();
                }
                return Ok(todoItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving pending todo items.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Creates a new todo item.
        /// Returns HTTP 200 OK if created successfully, HTTP 400 Bad Request for validation errors,
        /// or HTTP 500 with error details if an exception occurs.
        /// </summary>
        /// <param name="todoItemCreateDto">The todo item data to create.</param>
        /// <returns>An appropriate HTTP status code.</returns>
        [HttpPost]
        public async Task<ActionResult> CreateTodoItem([FromBody] CreateTodoItemDto todoItemCreateDto)
        {
            try
            {
                await _todoItemService.CreateTodoItemAsync(todoItemCreateDto);
                return Ok();
            }
            catch (ValidationException vex)
            {
                _logger.LogWarning(vex, "Validation failed when creating todo item.");
                return BadRequest(vex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating todo item.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        /// <summary>
        /// Marks the specified todo item as done.
        /// Returns HTTP 200 OK if successful, HTTP 404 Not Found if the item does not exist,
        /// or HTTP 500 with error details if an exception occurs.
        /// </summary>
        /// <param name="id">The ID of the todo item to mark as done.</param>
        /// <returns>An appropriate HTTP status code.</returns>
        [HttpPatch("{id}/done")]
        public async Task<IActionResult> MarkAsDone([FromRoute] int id)
        {
            try
            {
                var success = await _todoItemService.MarkAsDone(id);
                if (!success)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error marking todo item with ID: '{id}' as done.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}
