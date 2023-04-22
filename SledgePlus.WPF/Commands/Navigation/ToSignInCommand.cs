using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToSignInCommand : Command
{
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;

    public ToSignInCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
    }

    public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn;

    public override void Execute(object? parameter)
    {
        if (string.IsNullOrEmpty((parameter as User).Surname) ||
            string.IsNullOrEmpty((parameter as User).Name) ||
            string.IsNullOrEmpty((parameter as User).GroupId.ToString()) ||
            string.IsNullOrEmpty((parameter as User).RoleId.ToString())) return;
        _host.Services.GetRequiredService<SignInViewModel>().CurrentUser = parameter as User;
        _navigationStore.CurrentViewModel = _host.Services.GetRequiredService<SignInViewModel>();
    }
}