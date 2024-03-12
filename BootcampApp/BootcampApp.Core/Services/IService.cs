using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);

        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entity);

        Task UpdateAsync(T entity);
        Task RemoveAsync(T enitity);
        Task RemoveRangeAsync(IEnumerable<T> enitity);
    }
}
