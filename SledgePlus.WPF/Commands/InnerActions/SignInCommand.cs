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
        private readonly IHost _host;
        private readonly UsersService _userServices;

        public SignInCommand(IHost host, IMapper mapper)
        {
            _host = host;
            _userServices = (UsersService)host.Services.GetRequiredService<IDataServices<User>>();
        }

        public override bool CanExecute(object? parameter) =>
            Text.PasswordValidation(_host.Services.GetRequiredService<SignInViewModel>().Password)
            && _host.Services.GetRequiredService<SignInViewModel>().CanExecute();

        public override async void Execute(object? parameter)
        {
            var viewModel = _host.Services.GetRequiredService<SignInViewModel>();
            try
            {
                if (_userServices.GetByLogin(viewModel.Login) != null) throw new DuplicateException();
                var password = Cryptography.HashPassword(viewModel.Password);
                var user = viewModel.CurrentUser;
                user.Password = password;
                user.Login = viewModel.Login;
                await _userServices.Update(user);
                _host.Services.GetRequiredService<INavigationStore>().CurrentViewModel =
                    _host.Services.GetRequiredService<PersonalAccountViewModel>();
            }
            catch (DuplicateException)
            {
                viewModel.ErrorMessage = "Пользователь с данным логином уже существует";
            }
            catch (Exception)
            {
                viewModel.ErrorMessage = "Неизвестная ошибка";
            }
        }
    }
}