using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.ViewModels.UserControls.UserPanels.ModeratorPanels;

namespace SledgePlus.WPF.ViewModels.UserControls.UserPanels;

public class ModeratorPanelViewModel : ViewModel
{

    private ViewModel _currentPanel;

    public ViewModel CurrentPanel
    {
        get => _currentPanel;
        set => Set(ref _currentPanel, value);
    }

    public ModeratorPanelViewModel(IHost host)
    {
        CurrentPanel = host.Services.GetRequiredService<UsersTableViewModel>();
    }
}
