using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Models.Enumerators;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;

namespace SledgePlus.WPF.Commands.InnerActions;

public class SaveUsersListCommand : Command
{
    private readonly IHost _host;

    public SaveUsersListCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<ModeratorPanelViewModel>();
        var context = _host.Services.GetRequiredService<AppDbContext>();

        foreach (var vmChangedUser in vm.ChangedUsers)
        {
            if (vmChangedUser.RoleId == 0) vmChangedUser.RoleId = (int)Roles.Student;
            if (!(string.IsNullOrWhiteSpace(vmChangedUser.GroupId.ToString()) ||
                  string.IsNullOrWhiteSpace(vmChangedUser.Surname) ||
                  string.IsNullOrWhiteSpace(vmChangedUser.Name) ||
                  string.IsNullOrWhiteSpace(vmChangedUser.RoleId.ToString())))
                context.Users.Update(vmChangedUser);
        }

        context.SaveChanges();

        var uservm = _host.Services.GetRequiredService<AuthenticationViewModel>();
        var user = _host.Services.GetRequiredService<IDataServices<User>>().LogIn(uservm.Login, uservm.Password);
        
        _host.Services.GetRequiredService<ILoginStore>().CurrentUser = user;
    }
}