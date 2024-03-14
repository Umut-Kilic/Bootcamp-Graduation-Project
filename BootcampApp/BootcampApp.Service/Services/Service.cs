using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Repositories;
using BootcampApp.Service.Exceptions;
using NLayer.Core.Services;
using System.Linq.Expressions;

namespace BootcampApp.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {

        private readonly IGenericRepository<T> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }


        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"id alanı boş bırakılamaz");
            }
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) böyle bir ürün bulunamadı");
            }
            return entity;
        }

        public async Task RemoveAsync(T enitity)
        {
            _repository.Remove(enitity);
            await _unitOfWork.CommitAsync();

        }

        public async Task RemoveRangeAsync(IEnumerable<T> enitities)
        {
            _repository.RemoveRange(enitities);
            await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();

        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _repository.Where(expression);
        }
    }
}
