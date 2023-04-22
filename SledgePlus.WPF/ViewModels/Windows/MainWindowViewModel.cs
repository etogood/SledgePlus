using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.Navigation;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.Stores.WindowProperties;

namespace SledgePlus.WPF.ViewModels.Windows;

internal class MainWindowViewModel : ViewModel
{
    private readonly INavigationStore _navigationStore;
    private readonly ILoginStore _loginStore;
    private readonly IWindowPropertiesStore _windowPropertiesStore;

    public ICommand ToIDECommand { get; }
    public ICommand ToLearningMenuCommand { get; }
    public ICommand ToPersonalAccountCommand { get; }
    public ICommand QuitAccountCommand { get; }

    public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainWindowViewModel(IHost host)
    {
        ToIDECommand = host.Services.GetRequiredService<ToIDECommand>();
        ToLearningMenuCommand = host.Services.GetRequiredService<ToLearningMenuCommand>();
        ToPersonalAccountCommand = host.Services.GetRequiredService<ToPersonalAccountCommand>();
        QuitAccountCommand = host.Services.GetRequiredService<QuitAccountCommand>();

        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        _windowPropertiesStore = host.Services.GetRequiredService<IWindowPropertiesStore>();

        _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        _loginStore.IsLoggedIn = false;

        _windowPropertiesStore.HeightChanged += OnHeightChanged;
        _windowPropertiesStore.WidthChanged += OnWidthChanged;

        Height = 400;
        Width = 600;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

    private void OnHeightChanged()
    {
        OnPropertyChanged(nameof(Height));
    }

    private void OnWidthChanged()
    {
        OnPropertyChanged(nameof(Width));
    }
}