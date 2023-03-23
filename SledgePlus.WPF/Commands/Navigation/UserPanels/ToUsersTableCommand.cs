using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels.ModeratorPanels;

namespace SledgePlus.WPF.Commands.Navigation.UserPanels;

public class ToUsersTableCommand : Command
{
    private readonly IHost _host;

    public ToUsersTableCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        _host.Services.GetRequiredService<ModeratorPanelViewModel>().CurrentPanel =
            _host.Services.CreateScope().ServiceProvider.GetRequiredService<UsersTableViewModel>();
    }
}