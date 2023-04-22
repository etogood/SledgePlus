using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels.AdminPanels;

namespace SledgePlus.WPF.Commands.InnerActions;

public class RemoveUserRowCommand : Command
{
    private readonly IHost _host;

    public RemoveUserRowCommand(IHost host)
    {
        _host = host;
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<UsersTableViewModel>();
        try
        {
            var index = vm.Users.IndexOf(parameter as User ?? throw new Exception());
            if (index <= -1 || index >= vm.Users.Count) return;
            vm.Users.RemoveAt(index);
            var context = _host.Services.GetRequiredService<AppDbContext>();
            context.Users.Remove(parameter as User ?? throw new Exception());
            context.SaveChanges();
        }
        catch (Exception)
        {
            // ignore
        }
    }
}