using Microsoft.EntityFrameworkCore.Query;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DTOs;

namespace SledgePlus.WPF.Stores.Login;

public interface ILoginStore
{
    public User? CurrentUser { get; set; }

    bool IsLoggedIn { get; set; }

    event Action IsLoggedInChanged;
}