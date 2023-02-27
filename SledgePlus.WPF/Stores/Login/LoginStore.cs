using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Stores.Login;

public class LoginStore : ILoginStore
{
    private User? _currentUser;

    public User? CurrentUser
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