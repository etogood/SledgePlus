using SledgePlus.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SledgePlus.WPF.Models.DataServices;

public interface IDataServices<T>
{
    Task Create(T entity);

    Task Delete(T entity);

    Task Update(T oldEntity, T newEntity);

    Task<IEnumerable<T>> GetAll();

    public User LogIn(string login, string password);
}
