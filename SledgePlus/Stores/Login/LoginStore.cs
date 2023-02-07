using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Stores.Login;

public class LoginStore : ILoginStore
{
    private bool _isLoggedIn;

    public User? CurrentUser { get; set; }

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