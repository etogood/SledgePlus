using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.Windows;
using SledgePlus.WPF.Views.Windows;

namespace SledgePlus.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost _host = null!;

        public App()
        {
            _host = CreateHostBuilder().Build();
        }

        private static IHostBuilder CreateHostBuilder(string[]? args = null) => Host.CreateDefaultBuilder(args)
            .ConfigureServices(
                services =>
                {
                    // Stores
                    services.AddSingleton<INavigationStore, NavigationStore>();
                    services.AddSingleton<ILoginStore, LoginStore>();

                    // Windows
                    services.AddSingleton<MainWindow>();

                    // ViewModels
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddSingleton<AuthenticationViewModel>();
                });
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();
            //await _host.Services.GetRequiredService<AppDbContext>().Database.MigrateAsync();
            _host.Services.GetRequiredService<INavigationStore>().CurrentViewModel = _host.Services.GetRequiredService<AuthenticationViewModel>();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.DataContext = _host.Services.GetRequiredService<MainWindowViewModel>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
