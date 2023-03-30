using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DTOs;

namespace SledgePlus.WPF.Stores.Login;

public class LoginStore : ILoginStore
{
    private UserDTO? _currentUser;

    public UserDTO? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            _isLoggedIn = true;
            if (value == null) _isLoggedIn = false;
        }
    }

    private bool _isLoggedIn;

    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            _isLoggedIn = value;
            IsLoggedInChanged?.Invoke();
        }
    }

    public event Action? IsLoggedInChanged;
}