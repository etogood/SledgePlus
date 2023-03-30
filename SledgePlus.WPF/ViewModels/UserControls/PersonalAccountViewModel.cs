using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;

namespace SledgePlus.WPF.ViewModels.UserControls;

public class PersonalAccountViewModel : ViewModel
{
    #region Properties

    private string _surname;

    public string Surname
    {
        get => _surname;
        set => Set(ref _surname, value);
    }

    private string _name;

    public string Name
    {
        get => _name;
        set => Set(ref _name, value);
    }

    private string _patronymic;

    public string Patronymic
    {
        get => _patronymic;
        set => Set(ref _patronymic, value);
    }

    private string _group;

    public string Group
    {
        get => _group;
        set => Set(ref _group, value);
    }

    private ViewModel _userPanel;

    public ViewModel UserPanel
    {
        get => _userPanel;
        set => Set(ref _userPanel, value);
    }

    #endregion

    public PersonalAccountViewModel(IHost host)
    {
        var store = host.Services.GetRequiredService<ILoginStore>();
        Surname = store.CurrentUser.Surname;
        Name = store.CurrentUser.Name;
        Patronymic = store.CurrentUser.Patronymic;
        Group = store.CurrentUser.GroupGroupName;

        var role = store.CurrentUser.RoleRolePreferences;
        if (role.Contains('a') || role.Contains('A')) UserPanel = host.Services.GetRequiredService<AdminPanelViewModel>();
        else if (role.Contains('m') || role.Contains('M')) UserPanel = host.Services.GetRequiredService<ModeratorPanelViewModel>();
        else UserPanel = host.Services.GetRequiredService<StudentPanelViewModel>();
        
    }
}

/*
 Preferences:

    a - open admin panel for read
    m - open moderator panel for read
    A - open admin panel for write-read
    M - open admin panel for write-read

 */