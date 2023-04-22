using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels.AdminPanels;

namespace SledgePlus.WPF.Commands.Navigation.UserPanels;

public class ToAdminUsersTableCommand : Command
{
    private readonly IHost _host;

    public ToAdminUsersTableCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        _host.Services.GetRequiredService<AdminPanelViewModel>().CurrentPanel =
            _host.Services.GetRequiredService<UsersTableViewModel>();
    }
}