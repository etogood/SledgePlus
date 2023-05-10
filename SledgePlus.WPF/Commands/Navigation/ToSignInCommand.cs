﻿using System.Linq;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Stores.Login;
using SledgePlus.WPF.Stores.Navigation;
using SledgePlus.WPF.ViewModels.UserControls;

namespace SledgePlus.WPF.Commands.Navigation;

public class ToSignInCommand : Command
{
    private readonly AppDbContext _appDbContext;
    private readonly IHost _host;
    private readonly INavigationStore _navigationStore;

    public bool IsExecutable { get; set; }
    
    public ToSignInCommand(IHost host)
    {
        _host = host;
        _navigationStore = host.Services.GetRequiredService<INavigationStore>();
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();

        IsExecutable = true;
    }

    public override bool CanExecute(object? parameter)
    {
        if (parameter is not User user) return false;
        return !string.IsNullOrEmpty(user.Surname) &&
               !string.IsNullOrEmpty(user.Name) &&
               user.GroupId != 0 &&
               user.RoleId != 0 && 
               IsExecutable;
    }

    public override void Execute(object? parameter)
    {
        var vm = _host.Services.GetRequiredService<SignInViewModel>();
        try
        {
            _host.Services.GetRequiredService<SignInViewModel>().CurrentUser = parameter as User;
            vm.ErrorMessage =
                string.IsNullOrEmpty(_appDbContext.Users.ToList().FirstOrDefault(x => x == vm.CurrentUser)?.Password)
                    ? string.Empty
                    : "У этого пользователя уже есть данные для авторизации\nНажатие на кнопку ниже перезапишет их";

            _navigationStore.CurrentViewModel = vm;
        }
        catch (NullReferenceException)
        {
            MessageBox.Show("Сперва заполните данные о пользователе", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
        }
        catch (Exception)
        {
            //Do nothing
        }
    }
}