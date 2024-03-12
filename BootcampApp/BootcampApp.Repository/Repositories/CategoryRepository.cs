using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using System.Linq.Expressions;

namespace BootcampApp.Repository.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public Task AddAsync(Category entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(IEnumerable<Category> entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int? id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Category enitity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<Category> enitities)
        {
            throw new NotImplementedException();
        }

        public void Update(Category entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
