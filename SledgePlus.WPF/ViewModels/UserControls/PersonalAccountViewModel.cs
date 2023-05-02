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

    #endregion Properties

    public PersonalAccountViewModel(IHost host)
    {
        
    }
}