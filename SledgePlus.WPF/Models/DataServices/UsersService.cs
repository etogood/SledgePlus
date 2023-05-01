using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Models.Text;

namespace SledgePlus.WPF.Models.DataServices;

public class UsersService : IDataServices<User>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UsersService(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _mapper = host.Services.GetRequiredService<IMapper>();
    }

    public async Task Create(User user)
    {
        if (_appDbContext.Users.FirstOrDefault(user) == null) return; 
        await _appDbContext.Users.AddAsync(user);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(User user)
    {
        _appDbContext.Users.Update(user);
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
        return _appDbContext.Users.Include(x => x.Group).Include(x => x.Role).FirstOrDefault(x => string.Equals(login, x.Login));
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