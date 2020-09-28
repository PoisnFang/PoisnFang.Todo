using System.Collections.Generic;
using System.Threading.Tasks;
using PoisnFang.Todo.Entities;

namespace PoisnFang.Todo.Services
{
    public interface ITodoRepoService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllByRouteAsync(string route);

        Task<TEntity> GetByRouteAsync(string route);

        Task<TEntity> GetByIdAsync(int id);

        Task<TEntity> AddNewAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task DeleteAsync(int pkId);
    }
}