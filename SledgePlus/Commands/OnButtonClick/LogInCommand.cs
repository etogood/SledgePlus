using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.OnButtonClick;

public class LogInCommand : Command
{
    private readonly IHost _host;
    private readonly ILoginStore _loginStore;
    private readonly IFactory<ViewModel> _mediator;
    private readonly IDataServices<User> _userServices;

    public LogInCommand(IHost host)
    {
        _host = host;
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        _mediator = host.Services.GetRequiredService<IFactory<ViewModel>>();
        _userServices = host.Services.GetRequiredService<IDataServices<User>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = _mediator.Get(typeof(AuthenticationViewModel)) as AuthenticationViewModel;

        var user = _userServices.LogIn(vm.Login, vm.Password);

        if (user == null) throw new UserNotFoundException();
        _loginStore.CurrentUser = user;

        _userServices.Dispose();
    }
}