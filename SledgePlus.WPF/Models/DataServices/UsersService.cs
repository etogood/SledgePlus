using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Models.Text;

namespace SledgePlus.WPF.Models.DataServices;

public class UsersService : IDataServices<User>
{
    private readonly AppDbContext _appDbContext;

    public UsersService(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
    }

    public async Task Create(User user)
    {
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(User oldUser, User newUser)
    {
        oldUser.Login = newUser.Login;
        oldUser.Password = newUser.Password;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _appDbContext.Users.Remove(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        IEnumerable<User> users = await _appDbContext.Users.ToListAsync();
        return users;
    }

    public User? GetByLogin(string login)
    {
        return _appDbContext.Users.Include(x => x.Group).Include(x => x.Role).FirstOrDefault(x => x.Login == login);
    }

    public User LogIn(string login, string password)
    {
        var user = GetByLogin(login);
        if (user == null)
            throw new IncorrectLoginException("Не верный логин");
        if (!Cryptography.VerifyHashedPassword(user.Password, password))
            throw new IncorrectPasswordException("Не верный пароль");
        return user;
    }

    public void Dispose()
    {
        _appDbContext.Dispose();
    }
}