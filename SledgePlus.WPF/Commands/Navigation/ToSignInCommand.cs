using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToSignInCommand : Command
{
    private readonly AppDbContext _appDbContext;
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;

    public ToSignInCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>(); 
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();

    }

    public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn;

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<SignInViewModel>();
        if (string.IsNullOrEmpty((parameter as User).Surname) ||
            string.IsNullOrEmpty((parameter as User).Name) ||
            string.IsNullOrEmpty((parameter as User).GroupId.ToString()) ||
            string.IsNullOrEmpty((parameter as User).RoleId.ToString())) return;
        _host.Services.GetRequiredService<SignInViewModel>().CurrentUser = parameter as User;

        vm.ErrorMessage = string.IsNullOrEmpty(_appDbContext.Users.ToList().FirstOrDefault(x => x == vm.CurrentUser)?.Password)
            ? string.Empty
            : "У этого пользователя уже есть данные для авторизации\nНажатие на кнопку ниже перезапишет их";

        _navigationStore.CurrentViewModel = vm;
    }
}