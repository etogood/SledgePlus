using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.Base;

namespace SledgePlus.WPF.ViewModels.Windows
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly INavigationStore _navigationStore;
        //private readonly ILoginStore _loginStore;

        public ViewModel CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainWindowViewModel(IHost host)
        {
            _navigationStore = host.Services.GetRequiredService<INavigationStore>();

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
