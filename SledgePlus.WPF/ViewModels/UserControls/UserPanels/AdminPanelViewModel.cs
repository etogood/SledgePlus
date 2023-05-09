using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Commands.Navigation;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class AdminPanelViewModel : ViewModel
{
    public AppDbContext AppDbContext { get; set; }

    #region Properties

    public ICommand SaveUsersListCommand { get; set; }
    public ICommand ToSignInCommand { get; set; }
    public ICommand RemoveUserRowCommand { get; set; }
    public ICommand SearchCommand { get; set; }

    private string _searchQuery;
    public string SearchQuery
    {
        get => _searchQuery;
        set => Set(ref _searchQuery, value);
    }

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

    public AdminPanelViewModel(IHost host)
    {
        AppDbContext = host.Services.GetRequiredService<AppDbContext>();
        SaveUsersListCommand = host.Services.GetRequiredService<AdminSaveUsersListCommand>();
        ToSignInCommand = host.Services.GetRequiredService<ToSignInCommand>();
        RemoveUserRowCommand = host.Services.GetRequiredService<RemoveUserRowCommand>();
        SearchCommand = host.Services.GetRequiredService<AdminSearchCommand>();

        Users = new ObservableCollection<User>(AppDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group)
            .ToList());
        Groups = new ObservableCollection<Group>(AppDbContext.Groups.ToList());
        Roles = new ObservableCollection<Role>(AppDbContext.Roles.ToList());
    }
}