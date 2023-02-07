using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Stores.Login;

public interface ILoginStore
{
    public User? CurrentUser { get; set; }
    bool IsLoggedIn { get; set; }
    event Action IsLoggedInChanged;
}