using System.Linq;
using System.Windows.Input;

using Microsoft.Extensions.DependencyInjection;
using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Commands.InnerActions;

namespace SledgePlus.WPF.ViewModels.UserControls
{
    public class SignInViewModel : ViewModel
    {
        public ICommand SignInCommand { get; set; }

        
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

        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set => Set(ref _currentUser, value);
        }

        public string UserData {
        get
        {
            if (string.IsNullOrEmpty(CurrentUser.Surname) ||
                string.IsNullOrEmpty(CurrentUser.Name) ||
                string.IsNullOrEmpty(CurrentUser.GroupId.ToString()) ||
                string.IsNullOrEmpty(CurrentUser.RoleId.ToString())) 
                return string.Empty;
                    
            var userData = CurrentUser.Surname + ' ' + CurrentUser.Name + ' ' + CurrentUser.Patronymic;
            if (CurrentUser.Group != null) userData += ' ' + CurrentUser.Group.GroupName;
            return userData;
        }
    }

        public MessageViewModel ErrorMessageViewModel { get; }

        public string ErrorMessage
        {
            set => ErrorMessageViewModel.Message = value;
        }

        #endregion Properties

        public SignInViewModel(IHost host)
        {
            SignInCommand = host.Services.GetRequiredService<SignInCommand>();
            ErrorMessageViewModel = host.Services.GetRequiredService<MessageViewModel>();
            Password = string.Empty;
        }

        public bool CanExecute()
        {
            return !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(Login);
        }
    }
}