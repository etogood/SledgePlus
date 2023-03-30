using System.Collections.Generic;
using System.Threading.Tasks;

using SledgePlus.Data.Models;
using SledgePlus.WPF.Models.DTOs;

namespace SledgePlus.WPF.Models.DataServices;

public interface IDataServices<T>
{
    Task Create(T entity);

    Task Delete(T entity);

    Task Update(T oldEntity, T newEntity);

    Task<IEnumerable<T>> GetAll();

    public UserDTO LogIn(string login, string password);

    public void Dispose();
}