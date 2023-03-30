using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels.ModeratorPanels;

public class UsersTableViewModel : ViewModel
{
    private readonly AppDbContext _appDbContext;

	#region Properties

	private ObservableCollection<User> _users;

	public ObservableCollection<User> Users
	{
		get
        {
			return new ObservableCollection<User>(_appDbContext.Users
                .Include(x => x.Role)
                .Include(x => x.Group));
        }
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

    #endregion Properties

	public UsersTableViewModel(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();

        Users = new ObservableCollection<User>(_appDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group));
        Groups = new ObservableCollection<Group>(_appDbContext.Groups);
        Roles = new ObservableCollection<Role>(_appDbContext.Roles);
    }
}