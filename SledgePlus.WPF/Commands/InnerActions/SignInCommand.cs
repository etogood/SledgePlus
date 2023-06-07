using AutoMapper;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Models.Text;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;
using SledgePlus.WPF.ViewModels.UserControls.UserPanels;
using System.Collections.ObjectModel;
using SledgePlus.Data;

namespace SledgePlus.WPF.Commands.InnerActions
{
    internal class SignInCommand : Command
    {
        private readonly IHost _host;
        private readonly UsersService _userServices;
        private readonly ILoginStore _loginStore;

        public SignInCommand(IHost host, IMapper mapper)
        {
            _host = host;
            _loginStore = _host.Services.GetRequiredService<ILoginStore>();
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
                if (_loginStore.CurrentUser == null) return;
                var userLogin = _userServices.GetByLogin(viewModel.Login);
                if (userLogin != null && viewModel.CurrentUser != _loginStore.CurrentUser) throw new DuplicateException();

                var password = Cryptography.HashPassword(viewModel.Password);
                var user = viewModel.CurrentUser;

                user.Password = password;
                user.Login = viewModel.Login;

                await _userServices.Update(user);

                var context = _host.Services.GetRequiredService<AppDbContext>();
                _host.Services.GetRequiredService<ModeratorPanelViewModel>().Users = 
                    new ObservableCollection<User>(context.Users);
                _host.Services.GetRequiredService<AdminPanelViewModel>().Users =
                    new ObservableCollection<User>(context.Users);

                _host.Services.GetRequiredService<INavigationStore>().CurrentViewModel =
                    _host.Services.GetRequiredService<PersonalAccountViewModel>();

                MessageBox.Show("Данные для входа успешно обновлены");
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