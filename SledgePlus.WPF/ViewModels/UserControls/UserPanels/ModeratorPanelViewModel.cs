using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.Navigation.UserPanels;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class ModeratorPanelViewModel : ViewModel
{
    public ICommand ToUsersTableCommand { get; set; }

    private ViewModel _currentPanel;

    public ViewModel CurrentPanel
    {
        get => _currentPanel;
        set => Set(ref _currentPanel, value);
    }

    public ModeratorPanelViewModel(IHost host)
    {
        ToUsersTableCommand = host.Services.GetRequiredService<ToUsersTableCommand>();
    }
}