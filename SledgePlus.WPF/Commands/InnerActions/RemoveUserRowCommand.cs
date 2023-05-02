using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.Navigation;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using MessageBox = System.Windows.MessageBox;

namespace SledgePlus.WPF.Commands.InnerActions;

public class RemoveUserRowCommand : Command
{
    private readonly IHost _host;

    public RemoveUserRowCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) 
    {
        var vm = _host.Services.GetRequiredService<AdminPanelViewModel>();
        if (parameter is not User user) return false;
        if (string.IsNullOrEmpty(user.Surname) ||
            string.IsNullOrEmpty(user.Name) ||
            user.GroupId == 0 ||
            user.RoleId == 0) return false;
        var index = vm.Users.IndexOf(user);
        return !(index <= -1 || index >= vm.Users.Count);
    }

    public override void Execute(object? parameter)
    {
        if (MessageBox.Show("Вы уверены что хотите удалить данного пользоваетля?\n\nПри нажатии на кнопку \"Сохранить\" вас спросят об этом ещё раз", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.No) return;
        var vm = _host.Services.GetRequiredService<AdminPanelViewModel>();
        try
        {
            var index = vm.Users.IndexOf(parameter as User ?? throw new Exception());
            vm.Users.RemoveAt(index);
            var context = _host.Services.GetRequiredService<AppDbContext>();
            context.Users.Remove(parameter as User ?? throw new Exception());
        }
        catch (Exception)
        {
            // ignore
        }
    }
}