using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;

using SledgePlus.Data;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Commands.Navigation;
using SledgePlus.WPF.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class AdminPanelViewModel : ViewModel
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

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

    public AdminPanelViewModel(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _mapper = host.Services.GetRequiredService<IMapper>();

        SaveUsersListCommand = host.Services.GetRequiredService<AdminSaveUsersListCommand>();
        ToSignInCommand = host.Services.GetRequiredService<ToSignInCommand>();
        RemoveUserRowCommand = host.Services.GetRequiredService<RemoveUserRowCommand>();

        Users = new ObservableCollection<User>(_appDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group)
            .ToList());
        Groups = new ObservableCollection<Group>(_appDbContext.Groups.ToList());
        Roles = new ObservableCollection<Role>(_appDbContext.Roles.ToList());
    }
}