using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data.Models;

using SledgePlus.WPF.Models.Math;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.Stores.Navigation;

namespace SledgePlus.WPF.Commands.InnerActions
{
    internal class SignInCommand : Command
    {
        private readonly IHost _host;
        private readonly IFactory<ViewModel> _viewModelFactory;
        private readonly UsersService _userServices;
        private readonly INavigationStore _navigationStore;

        public SignInCommand(IHost host)
        {
            _host = host;
            _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
            _navigationStore = host.Services.GetRequiredService<INavigationStore>();
            _userServices = (UsersService)host.Services.GetRequiredService<IDataServices<User>>();
        }
        public override bool CanExecute(object? parameter) => 
            ((SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel))).CanExecute() 
            && Cryptography.PasswordValidation(((SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel))).Password);

        public override async void Execute(object? parameter)
        {
            var _viewModel = (SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel));
            try
            {
                if (_userServices.GetByLogin(_viewModel.Login) != null) throw new DuplicateException();
                var password = Cryptography.HashPassword(_viewModel.Password);
                var user = new User
                {
                    Login = _viewModel.Login,
                    Password = password
                };
                await _userServices.Create(user);
                _navigationStore.CurrentViewModel = _viewModelFactory.Get(typeof(AuthenticationViewModel));
            }
            catch (DuplicateException)
            {
                _viewModel.ErrorMessage = "Пользователь с данным логином уже существует";
            }
            
        }
    }
}
