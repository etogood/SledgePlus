using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Models.Text;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.InnerActions
{
    internal class SignInCommand : Command
    {
        private readonly IFactory<ViewModel> _viewModelFactory;
        private readonly UsersService _userServices;
        private readonly INavigationStore _navigationStore;
        private readonly IMapper _mapper;

        public SignInCommand(IHost host, IMapper mapper)
        {
            _mapper = mapper;
            _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
            _navigationStore = host.Services.GetRequiredService<INavigationStore>();
            _userServices = (UsersService)host.Services.GetRequiredService<IDataServices<User>>();
        }

        public override bool CanExecute(object? parameter) =>
            ((SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel))).CanExecute()
            && Text.PasswordValidation(((SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel))).Password);

        public override async void Execute(object? parameter)
        {
            var _viewModel = (SignInViewModel)_viewModelFactory.Get(typeof(SignInViewModel));
            try
            {
                if (_userServices.GetByLogin(_viewModel.Login) != null) throw new DuplicateException();
                var password = Cryptography.HashPassword(_viewModel.Password);
                var user = _viewModel.CurrentUser;
                user.Password = password;
                user.Login = _viewModel.Login;
                await _userServices.Update(user);
            }
            catch (DuplicateException)
            {
                _viewModel.ErrorMessage = "Пользователь с данным логином уже существует";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Неизвестная ошибка";
            }
        }
    }
}