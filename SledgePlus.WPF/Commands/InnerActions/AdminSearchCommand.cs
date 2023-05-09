using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using SledgePlus.WPF.Views.Windows;

namespace SledgePlus.WPF.Commands.InnerActions;

public class AdminSearchCommand : Command
{
    private readonly IHost _host;

    public bool IsExecutable;

    public AdminSearchCommand(IHost host)
    {
        _host = host;

        IsExecutable = true;
    }

    public override bool CanExecute(object? parameter) => IsExecutable;

    public override async void Execute(object? parameter)
    {
        _host.Services.GetRequiredService<IndeterminateProgressBarWindow>().Show();

        await Task.Run(Build);

        _host.Services.GetRequiredService<IndeterminateProgressBarWindow>().Hide();
    }

    private async Task Build()
    {
        IsExecutable = false;

        var vm = _host.Services.GetRequiredService<AdminPanelViewModel>();

        vm.Users = new ObservableCollection<User>(await _host.Services.GetRequiredService<AppDbContext>().Users
            .Include(x => x.Role)
            .Include(x => x.Group)
            .Where(x => x.Name.Contains(vm.SearchQuery)
                        || x.Surname.Contains(vm.SearchQuery)
                        || x.Patronymic.Contains(vm.SearchQuery)
                        || x.Group.GroupName.Contains(vm.SearchQuery))
            .ToListAsync());

        IsExecutable = true;
    }
}