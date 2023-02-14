using System;

using SledgePlus.WPF.ViewModels.Base;

namespace SledgePlus.WPF.Stores.Navigation;

public class NavigationStore : INavigationStore
{
    private ViewModel _currentViewModel = null!;

    public ViewModel CurrentViewModel
    {
        get => _currentViewModel;
        set
        {
            _currentViewModel = value;
            CurrentViewModelChanged?.Invoke();
        }
    }

    public event Action? CurrentViewModelChanged;
}