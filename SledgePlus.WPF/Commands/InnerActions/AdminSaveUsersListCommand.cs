using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Models.Enumerators;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;

namespace SledgePlus.WPF.Commands.InnerActions;

public class AdminSaveUsersListCommand : Command
{
    private readonly IHost _host;

    public AdminSaveUsersListCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<AdminPanelViewModel>();
        var context = _host.Services.GetRequiredService<AppDbContext>();
        try
        {
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
        }
        catch (Exception)
        {
            
        }
        

        
    }
}