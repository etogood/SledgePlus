using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToLearningMenuCommand : Command
{
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;

    public ToLearningMenuCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
    }

    public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn;

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = 
            _host.Services.CreateScope().ServiceProvider.GetRequiredService<LearningMenuViewModel>();
    }
}