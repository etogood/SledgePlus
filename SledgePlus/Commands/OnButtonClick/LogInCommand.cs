using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;

namespace SledgePlus.WPF.Commands.OnButtonClick;

public class LogInCommand : Command
{
    private readonly IHost _host;
    private readonly ILoginStore _loginStore;
    private readonly INavigationStore _navigationStore;
    private readonly IDataServices<User> _userServices;


    public LogInCommand(IHost host)
    {
        _host = host;
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _userServices = host.Services.GetRequiredService<IDataServices<User>>();
    }
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        throw new NotImplementedException();
    }
}