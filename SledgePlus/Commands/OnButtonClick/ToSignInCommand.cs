using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.OnButtonClick;

public class ToSignInCommand : Command
{
    private readonly IFactory<ViewModel> _viewModelFactory;
    private readonly INavigationStore _navigationStore;

    public ToSignInCommand(IHost host)
    {
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        _navigationStore.CurrentViewModel = _viewModelFactory.Get(typeof(SignInViewModel));
    }
}