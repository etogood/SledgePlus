using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.Data;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Commands.Navigation;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class ModeratorPanelViewModel : ViewModel
{
    public AppDbContext AppDbContext { get; set; }

    #region Properties

    public ICommand SaveUsersListCommand { get; set; }
    public ICommand ToSignInCommand { get; set; }
    public ICommand RemoveUserRowCommand { get; set; }

    private ObservableCollection<User> _users;

    public ObservableCollection<User> Users
    {
        get => _users;
        set => Set(ref _users, value);
    }

    private ObservableCollection<Group> _groups;

    public ObservableCollection<Group> Groups
    {
        get => _groups;
        set => Set(ref _groups, value);
    }

    private ObservableCollection<Role> _roles;

    public ObservableCollection<Role> Roles
    {
        get => _roles;
        set => Set(ref _roles, value);
    }

    private User? _selectedRow;

    public User? SelectedRow
    {
        get => _selectedRow;
        set => Set(ref _selectedRow, value);
    }

    private ObservableCollection<User> _changedUsers;

    public ObservableCollection<User> ChangedUsers
    {
        get => _changedUsers;
        set => Set(ref _changedUsers, value);
    }

    #endregion Properties

    public ModeratorPanelViewModel(IHost host)
    {
        AppDbContext = host.Services.GetRequiredService<AppDbContext>();

        SaveUsersListCommand = host.Services.GetRequiredService<AdminSaveUsersListCommand>();
        ToSignInCommand = host.Services.GetRequiredService<ToSignInCommand>();
        RemoveUserRowCommand = host.Services.GetRequiredService<RemoveUserRowCommand>();

        Users = new ObservableCollection<User>(AppDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group)
            .ToList());
        Groups = new ObservableCollection<Group>(AppDbContext.Groups.ToList());
        Roles = new ObservableCollection<Role>(AppDbContext.Roles.ToList());
    }
}