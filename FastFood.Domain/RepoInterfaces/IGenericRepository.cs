using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Domain.RepoInterfaces
{
    // generics are used if you dont know what functions are gonna be used
    // below interface T is used to take in any class
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetById(int id);
        Task<ICollection<T>> GetAll();
        Task<bool> Exists(int id);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Remove(T entity);
        Task<bool> RemoveMany(ICollection<T> entities);
    }
}
