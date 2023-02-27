﻿using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;

using SledgePlus.WPF.Commands.InnerActions;

namespace SledgePlus.WPF.ViewModels.UserControls
{
    public class SignInViewModel : ViewModel
    {
        public ICommand SignInCommand { get; set; }

        public MessageViewModel ErrorMessageViewModel { get; }

        #region Properties

        private string _login;

        public string Login
        {
            get => _login;
            set => Set(ref _login, value);
        }

        private string _password;

        public string Password
        {
            get => _password;
            set => Set(ref _password, value);
        }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        #endregion Properties

        public SignInViewModel(IHost host)
        {
            SignInCommand = host.Services.GetRequiredService<SignInCommand>();
            ErrorMessageViewModel = host.Services.GetRequiredService<MessageViewModel>();
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }
    }
}