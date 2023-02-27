using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Models.Math;

namespace SledgePlus.WPF.Models.DataServices;

public class UsersService : IDataServices<User>
{
    private readonly AppDbContext _context;

    public UsersService(IHost host)
    {
        _context = host.Services.GetRequiredService<AppDbContext>();
    }

    public async Task Create(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User oldUser, User newUser)
    {
        oldUser.Login = newUser.Login;
        oldUser.Password = newUser.Password;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        IEnumerable<User> users = await _context.Users.ToListAsync();
        return users;
    }

    public User? GetByLogin(string login)
    {
        return _context.Users.FirstOrDefault(x => x.Login == login);
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
        _context.Dispose();
    }
}