using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using TrackIt.API.Controllers;
using TrackIt.Application.DTOs;
using TrackIt.Application.Services;

namespace TrackIt.Tests.Tests
{
    [TestClass]
    public class TodoItemControllerTests
    {
        private Mock<ITodoItemService> _mockTodoItemService;
        private Mock<ILogger<TodoItemController>> _mockLogger;
        private TodoItemController _todoItemController;

        [TestInitialize]
        public void Setup()
        {
            _mockTodoItemService = new Mock<ITodoItemService>();
            _mockLogger = new Mock<ILogger<TodoItemController>>();
            _todoItemController = new TodoItemController(_mockTodoItemService.Object, _mockLogger.Object);
        }

        [TestMethod]
        public async Task GetPendingTodoItems_ReturnsNoContent_WhenNoneFound()
        {
            // ARRANGE
            _mockTodoItemService.Setup(s => s.GetPendingTodoItemsAsync()).ReturnsAsync([]);

            // ACT
            var result = await _todoItemController.GetPendingTodoItems();

            // ASSERT
            Assert.IsInstanceOfType(result.Result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task GetPendingTodoItems_ReturnsOk_WithItems()
        {
            // ARRANGE
            var items = new List<TodoItemDto> { new() { Name = "Test" } };
            _mockTodoItemService.Setup(s => s.GetPendingTodoItemsAsync()).ReturnsAsync(items);

            // ACT
            var result = await _todoItemController.GetPendingTodoItems();

            // ASSERT
            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(items, okResult.Value);
        }

        [TestMethod]
        public async Task CreateTodoItem_ReturnsOk()
        {
            // ARRANGE
            var dto = new CreateTodoItemDto() { Name = "Test" };
            _mockTodoItemService.Setup(s => s.CreateTodoItemAsync(dto)).Returns(Task.CompletedTask);

            // ACT
            var result = await _todoItemController.CreateTodoItem(dto);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(OkResult));
            _mockTodoItemService.Verify(s => s.CreateTodoItemAsync(dto), Times.Once);
        }

        [TestMethod]
        public async Task MarkAsDone_ReturnsNotFound_WhenItemDoesNotExist()
        {
            // ARRANGE
            int id = 1;
            _mockTodoItemService.Setup(s => s.MarkAsDone(id)).ReturnsAsync(false);

            // ACT
            var result = await _todoItemController.MarkAsDone(id);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task MarkAsDone_ReturnsOk_WhenTodoItemIsMarkedDone()
        {
            // ARRANGE
            int id = 1;
            _mockTodoItemService.Setup(s => s.MarkAsDone(id)).ReturnsAsync(true);

            // ACT
            var result = await _todoItemController.MarkAsDone(id);

            // ASSERT
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }
    }
}
