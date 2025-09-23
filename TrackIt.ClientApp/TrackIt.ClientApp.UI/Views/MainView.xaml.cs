using System.Windows;
using TrackIt.ClientApp.Application.Interfaces;
using TrackIt.ClientApp.UI.ViewModels;

namespace TrackIt.ClientApp.UI.Views
{
    public partial class MainView : Window
    {
        public MainView(ITodoItemService todoItemService)
        {
            InitializeComponent();
            DataContext = new MainViewModel(todoItemService);
        }
    }
}
