using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.Views.Windows;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToLearningMenuCommand : Command
{
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;
    private readonly LearningMenuViewModel _vm;

    public bool IsExecutable { get; set; }

    public ToLearningMenuCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        _vm = _host.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<LearningMenuViewModel>();
        IsExecutable = true;
    }

    public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn && IsExecutable;

    public override async void Execute(object? parameter)
    {
        _host.Services.GetRequiredService<IndeterminateProgressBarWindow>().Show();

        _navigationStore.CurrentViewModel = _vm;
        await Task.Run(Build);

        _host.Services.GetRequiredService<IndeterminateProgressBarWindow>().Hide();
    }

    private async Task Build()
    {
        IsExecutable = false;
        _host.Services.GetRequiredService<ToPersonalAccountCommand>().IsExecutable = false;
        _host.Services.GetRequiredService<ToWelcomeCommand>().IsExecutable = false;
        _host.Services.GetRequiredService<ToIDECommand>().IsExecutable = false;
        _host.Services.GetRequiredService<QuitAccountCommand>().IsExecutable = false;

        await _vm.Build();

        IsExecutable = true;
        _host.Services.GetRequiredService<ToPersonalAccountCommand>().IsExecutable = true;
        _host.Services.GetRequiredService<ToWelcomeCommand>().IsExecutable = true;
        _host.Services.GetRequiredService<ToIDECommand>().IsExecutable = true;
        _host.Services.GetRequiredService<QuitAccountCommand>().IsExecutable = true;
    }
}