using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.Enumerators;
using SledgePlus.WPF.Stores.Login;
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

        if (MessageBox.Show(
                "При нажатии, все удалённые или изменённые вами данные будут потеряны без возможности восстановления\n\nПродолжить?",
                "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) ==
            MessageBoxResult.No) return;

        
            foreach (var vmChangedUser in vm.ChangedUsers)
            {
                if (vmChangedUser.DeleteFlag && vmChangedUser != _host.Services.GetRequiredService<ILoginStore>().CurrentUser)
                {
                    try { context.Users.Remove(vmChangedUser); }
                    catch (Exception) { MessageBox.Show($"Пользователя {vmChangedUser.Fullname} невозможно удалить, так как он существует исключительно локально"); }
                    continue;
                }

                if (vmChangedUser.RoleId == 0) vmChangedUser.RoleId = (int)Roles.Student;
                if (!(string.IsNullOrWhiteSpace(vmChangedUser.GroupId.ToString()) ||
                      string.IsNullOrWhiteSpace(vmChangedUser.Surname) ||
                      string.IsNullOrWhiteSpace(vmChangedUser.Name) ||
                      string.IsNullOrWhiteSpace(vmChangedUser.RoleId.ToString())))

                    context.Users.Update(vmChangedUser);
            }
            context.SaveChanges();

            vm.Users = new ObservableCollection<User>(context.Users);

    }
}