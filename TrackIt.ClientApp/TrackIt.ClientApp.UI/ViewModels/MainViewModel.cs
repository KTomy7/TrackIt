using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TrackIt.ClientApp.Application.Interfaces;
using TrackIt.ClientApp.Domain.Entities;
using TrackIt.ClientApp.Domain.Enums;
using TrackIt.ClientApp.UI.Commands;

namespace TrackIt.ClientApp.UI.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ITodoItemService _todoItemService;
        public ObservableCollection<TodoItem> TodoItems { get; } = [];

        private string _newTodoItemName = string.Empty;
        public string NewTodoItemName
        {
            get => _newTodoItemName;
            set { _newTodoItemName = value; OnPropertyChanged(); }
        }

        private string? _newTodoItemDescription;
        public string? NewTodoItemDescription
        {
            get => _newTodoItemDescription;
            set { _newTodoItemDescription = value; OnPropertyChanged(); }
        }

        private PriorityEnum _newTodoItemPriority;
        public PriorityEnum NewTodoItemPriority
        {
            get => _newTodoItemPriority;
            set { _newTodoItemPriority = value; OnPropertyChanged(); }
        }

        public static Array PriorityValues => Enum.GetValues(typeof(PriorityEnum));

        public ICommand CreateTodoItemCommand { get; }
        public ICommand MarkAsDoneCommand { get; }

        public MainViewModel(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
            CreateTodoItemCommand = new RelayCommand(async _ => await CreateTodoItemAsync(), _ => !string.IsNullOrWhiteSpace(NewTodoItemName));
            MarkAsDoneCommand = new RelayCommand(async id => await MarkAsDoneAsync((int)id!));
            _ = GetPendingTodoItemsAsync();
        }

        private async Task GetPendingTodoItemsAsync()
        {
            TodoItems.Clear();
            var items = await _todoItemService.GetPendingTodoItemsAsync();
            foreach (var item in items)
            {
                TodoItems.Add(item);
            }
        }

        private async Task CreateTodoItemAsync()
        {
            var newItem = new CreateTodoItem
            {
                Name = NewTodoItemName,
                Description = NewTodoItemDescription,
                Priority = NewTodoItemPriority,
                CreatedAt = DateTime.Now
            };

            await _todoItemService.CreateTodoItemAsync(newItem);
            await GetPendingTodoItemsAsync();

            NewTodoItemName = string.Empty;
            NewTodoItemDescription = null;
            NewTodoItemPriority = PriorityEnum.Medium;
        }

        private async Task MarkAsDoneAsync(int id)
        {
            await _todoItemService.MarkAsDoneAsync(id);
            await GetPendingTodoItemsAsync();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
