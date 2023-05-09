using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Xml.Linq;
using SledgePlus.Data;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToPersonalAccountCommand : Command
{
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;

    public bool IsExecutable;

    public ToPersonalAccountCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        IsExecutable = true;
    }

    public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn && IsExecutable;

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<PersonalAccountViewModel>();
        var store = _host.Services.GetRequiredService<ILoginStore>();
        vm.Surname = store.CurrentUser.Surname;
        vm.Name = store.CurrentUser.Name;
        vm.Patronymic = store.CurrentUser.Patronymic;
        vm.Group = store.CurrentUser.Group.GroupName;

        var role = store.CurrentUser.Role.RolePreferences;
        if (role.Contains('a') || role.Contains('A'))
        {
            _host.Services.GetRequiredService<AdminPanelViewModel>().AppDbContext = new AppDbContext();
            vm.UserPanel = _host.Services.GetRequiredService<AdminPanelViewModel>();
        }
        else if (role.Contains('m') || role.Contains('M'))
        {
            _host.Services.GetRequiredService<ModeratorPanelViewModel>().AppDbContext = new AppDbContext();
            vm.UserPanel = _host.Services.GetRequiredService<ModeratorPanelViewModel>();
        }
        else vm.UserPanel = _host.Services.GetRequiredService<StudentPanelViewModel>();
        _navigationStore.CurrentViewModel =
            _host.Services.GetRequiredService<PersonalAccountViewModel>();
    }
}