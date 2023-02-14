using System.Windows.Input;
using System.Windows.Media;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SledgePlus.WPF.Commands.OnButtonClick;

namespace SledgePlus.WPF.ViewModels.UserControls;

internal sealed class AuthenticationViewModel : ViewModel
{
    private MessageViewModel _messageViewModel;

    public ICommand LogInCommand { get; }
    public ICommand ToSignInCommand { get; }


    public string Message
    {
        get => _messageViewModel.Message; 
        set => _messageViewModel.Message = value;
    }

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
    #endregion


    public AuthenticationViewModel(IHost host)
    {
        Height = 360;
        Width  = 200;
        LogInCommand = host.Services.GetRequiredService<LogInCommand>();
        ToSignInCommand = host.Services.GetRequiredService<ToSignInCommand>();
        _messageViewModel = host.Services.GetRequiredService<MessageViewModel>();
    }
}
