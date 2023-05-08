using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Base
{
    public class QuitAccountCommand : Command
    {
        private readonly ILoginStore _loginStore;
        private readonly INavigationStore _navigationStore;
        private readonly IHost _host;

        public QuitAccountCommand(IHost host)
        {
            _host = host;
            _loginStore = host.Services.GetRequiredService<ILoginStore>();
            _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        }

        public override bool CanExecute(object? parameter) => _loginStore.IsLoggedIn;

        public override void Execute(object? parameter)
        {
            _loginStore.CurrentUser = null;
            _host.Services.GetRequiredService<AuthenticationViewModel>().Login = string.Empty;
            _host.Services.GetRequiredService<AuthenticationViewModel>().Password = string.Empty;
            _navigationStore.CurrentViewModel = _host.Services.GetRequiredService<AuthenticationViewModel>();
        }
    }
}