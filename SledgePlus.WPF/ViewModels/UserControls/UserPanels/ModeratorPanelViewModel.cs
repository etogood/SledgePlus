using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.Data;
using SledgePlus.WPF.Commands.InnerActions;
using SledgePlus.WPF.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class ModeratorPanelViewModel : ViewModel
{

    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    #region Properties

    public ICommand SaveUsersListCommand { get; set; }

    private ObservableCollection<User> _users;

    public ObservableCollection<User> Users
    {
        get => _users;
        set => Set(ref _users, value);
    }

    private ObservableCollection<GroupDTO> _groups;

    public ObservableCollection<GroupDTO> Groups
    {
        get => _groups;
        set => Set(ref _groups, value);
    }

    private ObservableCollection<RoleDTO> _roles;

    public ObservableCollection<RoleDTO> Roles
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
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _mapper = host.Services.GetRequiredService<IMapper>();

        SaveUsersListCommand = host.Services.GetRequiredService<SaveUsersListCommand>();

        Users = new ObservableCollection<User>(_appDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group));
        Groups = new ObservableCollection<GroupDTO>(_mapper.Map<IEnumerable<Group>, IEnumerable<GroupDTO>>(_appDbContext.Groups));
        Roles = new ObservableCollection<RoleDTO>(_mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_appDbContext.Roles));
    }
}