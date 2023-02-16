using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MySql.Data.MySqlClient;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Factories;
using SledgePlus.WPF.Models.DataServices;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.InnerActions;

public class LogInCommand : Command
{
    private readonly ILoginStore _loginStore;
    private readonly IFactory<ViewModel> _viewModelFactory;
    private readonly IDataServices<User> _userServices;

    public LogInCommand(IHost host)
    {
        _loginStore = host.Services.GetRequiredService<ILoginStore>();
        _viewModelFactory = host.Services.GetRequiredService<IFactory<ViewModel>>();
        _userServices = host.Services.GetRequiredService<IDataServices<User>>();
    }

    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        var vm = _viewModelFactory.Get(typeof(AuthenticationViewModel)) as AuthenticationViewModel;
        vm.ErrorMessage = string.Empty;
        try
        {
            var user = _userServices.LogIn(vm.Login, vm.Password);

            if (user == null) throw new UserNotFoundException();
            _loginStore.CurrentUser = user;

            _userServices.Dispose();
        }
        catch (UserNotFoundException)
        {
            vm.ErrorMessage = "Пользователь не найден";
        }
        catch (IncorrectLoginException)
        {
            vm.ErrorMessage = "Не верный логин";
        }
        catch (IncorrectPasswordException)
        {
            vm.ErrorMessage = "Не верный пароль";
        }
        catch (ArgumentNullException)
        {
            vm.ErrorMessage = "Не допускается пустое значение";
        }
        catch (MySqlException)
        {
            vm.ErrorMessage = "Ошибка подключения к базе даных";
        }
        catch (Exception)
        {
            vm.ErrorMessage = "Неизвестная ошибка";
        }
    }
}