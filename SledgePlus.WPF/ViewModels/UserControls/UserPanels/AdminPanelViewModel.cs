using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.Navigation.UserPanels;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class AdminPanelViewModel : ViewModel
{
    public ICommand ToAdminUsersTableCommand { get; set; }

    private ViewModel _currentPanel;

    public ViewModel CurrentPanel
    {
        get => _currentPanel;
        set => Set(ref _currentPanel, value);
    }

    public AdminPanelViewModel(IHost host)
    {
        ToAdminUsersTableCommand = host.Services.GetRequiredService<ToAdminUsersTableCommand>();
    }
}