using Microsoft.Extensions.DependencyInjection;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToIDECommand : Command
{
    private readonly INavigationStore _navigationStore;
    private readonly IFactory<ViewModel> _viewModelFactory;

    public ToIDECommand(IHost host)
    {
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = _viewModelFactory.Get(typeof(IDEViewModel));
    }
}