using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToWelcomeCommand : Command
{
    private readonly IHost _host;

    public ToWelcomeCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => _host.Services.GetRequiredService<ILoginStore>().IsLoggedIn;

    public override void Execute(object? parameter)
    {
        _host.Services.GetRequiredService<INavigationStore>().CurrentViewModel =
            _host.Services.GetRequiredService<WelcomeViewModel>();
    }
}