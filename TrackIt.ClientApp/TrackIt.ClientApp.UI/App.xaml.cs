using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TrackIt.ClientApp.Application.Interfaces;
using TrackIt.ClientApp.Infrastructure.Services;
using TrackIt.ClientApp.UI.Views;

namespace TrackIt.ClientApp.UI
{
    public partial class App : System.Windows.Application
    {
        private ServiceProvider? _serviceProvider;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var services = new ServiceCollection();

            // Register services
            services.AddHttpClient<ITodoItemService, TodoItemService>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:7258/");
            });

            // Register MainView
            services.AddTransient<MainView>();

            _serviceProvider = services.BuildServiceProvider();

            var mainView = _serviceProvider.GetRequiredService<MainView>();
            mainView.Show();
        }
    }
}
