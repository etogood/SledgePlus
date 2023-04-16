using System.Collections.Generic;
using System.Threading.Tasks;
using SledgePlus.Data.Models;

namespace SledgePlus.WPF.Models.DataServices;

public interface IDataServices<T>
{
    Task Create(T entity);

    Task Delete(T entity);

    Task Update(T entity);

    Task<IEnumerable<T>> GetAll();

    public User LogIn(string login, string password);

    public void Dispose();
}