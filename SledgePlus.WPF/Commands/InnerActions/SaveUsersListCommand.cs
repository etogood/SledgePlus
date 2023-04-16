using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DTOs;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels.ModeratorPanels;

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
        var vm = _host.Services.GetRequiredService<UsersTableViewModel>();
        var context = _host.Services.GetRequiredService<AppDbContext>();
        var mapper = _host.Services.GetRequiredService<IMapper>();

        foreach (var vmChangedUser in vm.ChangedUsers) context.Users.Update(vmChangedUser);
        context.SaveChanges();
    }
}