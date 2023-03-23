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

    #endregion Properties

	public UsersTableViewModel(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();

        Users = new ObservableCollection<User>(_appDbContext.Users
            .Include(x => x.Role)
            .Include(x => x.Group));
    }
}