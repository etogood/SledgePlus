using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SledgePlus.Data;
using SledgePlus.Data.Models;
using SledgePlus.WPF.Exceptions;
using SledgePlus.WPF.Models.DTOs;
using SledgePlus.WPF.Models.Text;

namespace SledgePlus.WPF.Models.DataServices;

public class UsersService : IDataServices<UserDTO>
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public UsersService(IHost host)
    {
        _appDbContext = host.Services.GetRequiredService<AppDbContext>();
        _mapper = host.Services.GetRequiredService<IMapper>();
    }

    public async Task Create(UserDTO user)
    {
        await _appDbContext.Users.AddAsync(_mapper.Map<UserDTO, User>(user));
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Update(UserDTO oldUser, UserDTO newUser)
    {
        oldUser.Login = newUser.Login;
        oldUser.Password = newUser.Password;
        await _appDbContext.SaveChangesAsync();
    }

    public async Task Delete(UserDTO user)
    {
        _appDbContext.Users.Remove(_mapper.Map<UserDTO, User>(user));
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        IEnumerable<UserDTO> users = await _mapper.Map<DbSet<User>, DbSet<UserDTO>>(_appDbContext.Users).ToListAsync();
        return users;
    }

    public UserDTO? GetByLogin(string login)
    {
        return _mapper.Map<User?, UserDTO?>(_appDbContext.Users.Include(x => x.Group).Include(x => x.Role).FirstOrDefault(x => x.Login == login));
    }

    public UserDTO LogIn(string login, string password)
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