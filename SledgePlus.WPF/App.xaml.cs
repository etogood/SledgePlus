using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Commands.Navigation;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.Stores.WindowProperties;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.Custom;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using SledgePlus.WPF.ViewModels.Windows;
using SledgePlus.WPF.Views.Windows;

namespace SledgePlus.WPF;

public partial class App
{
    private static readonly IHost Host = CreateHostBuilder().Build();

    private static IHostBuilder CreateHostBuilder(string[]? args = null) => Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
        .ConfigureServices(
            services =>
            {
                // Database
                services.AddDbContext<AppDbContext>();
                services.AddAutoMapper(typeof(App));

                // Stores
                services.AddSingleton<INavigationStore, NavigationStore>();
                services.AddSingleton<ILoginStore, LoginStore>();
                services.AddScoped<IWindowPropertiesStore, WindowPropertiesStore>();

                // Mediators
                services.AddSingleton<IFactory<ViewModel>, ViewModelFactory>();

                // Commands
                services.AddSingleton<CloseApplicationCommand>();
                services.AddSingleton<QuitAccountCommand>();

                services.AddSingleton<LogInCommand>();
                services.AddSingleton<SignInCommand>();
                services.AddSingleton<CompileCodeCommand>();
                services.AddSingleton<RunCodeCommand>();
                services.AddSingleton<SaveUsersListCommand>();
                services.AddSingleton<AdminSaveUsersListCommand>();
                services.AddSingleton<RemoveUserRowCommand>();

                services.AddSingleton<ToSignInCommand>();
                services.AddSingleton<ToIDECommand>();
                services.AddSingleton<ToLearningMenuCommand>();
                services.AddSingleton<ToPersonalAccountCommand>();

                // Models
                services.AddSingleton<IDataServices<User>, UsersService>();

                // Views
                services.AddSingleton<MainWindow>();

                // ViewModels
                services.AddTransient<MessageViewModel>();
                services.AddSingleton<MainWindowViewModel>();
                services.AddScoped<AuthenticationViewModel>();
                services.AddScoped<UserMenuViewModel>();
                services.AddScoped<SignInViewModel>();
                services.AddSingleton<PersonalAccountViewModel>();
                services.AddScoped<IDEViewModel>();
                services.AddScoped<WelcomeViewModel>();
                services.AddScoped<LearningMenuViewModel>();

                services.AddScoped<LessonItemViewModel>();
                services.AddScoped<ExpanderLessonItemViewModel>();

                services.AddSingleton<StudentPanelViewModel>();
                services.AddSingleton<ModeratorPanelViewModel>();
                services.AddSingleton<AdminPanelViewModel>();
            });

    protected override async void OnStartup(StartupEventArgs e)
    {
        try
        {
            await Host.StartAsync();
            await Host.Services.GetRequiredService<AppDbContext>().Database.MigrateAsync();
            Host.Services.GetRequiredService<INavigationStore>().CurrentViewModel =
                Host.Services.GetRequiredService<AuthenticationViewModel>();
        }
        catch (MySqlException)
        {
            MessageBox.Show("Произошла ошибка при попытке подключения к базе данных, проверьте подключение к сети или обратитесь к системному администратору", "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        MainWindow = Host.Services.GetRequiredService<MainWindow>();
        MainWindow.DataContext = Host.Services.GetRequiredService<MainWindowViewModel>();
        MainWindow.Show();

        base.OnStartup(e);
            
            
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await Host.StopAsync();
        Host.Dispose();

        base.OnExit(e);
    }
}