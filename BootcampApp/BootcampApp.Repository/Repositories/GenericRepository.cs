using BootcampApp.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BootcampApp.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Protected dememızın sebebi başka entityler bu generic repoyu kalıtım aldıgında onlarda bu dbContexte ulaşabilsinler diye
        protected readonly BootcampAppDbContext _context;

        //readonly  olarak tanımlama sebebimiz bu değişkenleri ya ilk tanımlı yerde değer atabiliriz veya constroctorde set edebilirz.
        //Basşa yerlerde set etmeye kalkarsak hata alırız
        private readonly DbSet<T> _dbSet;


        public GenericRepository(BootcampAppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);


        }

        public async Task AddRangeAsync(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return _dbSet.AnyAsync(expression);
        }

        public void Remove(T enitity)
        {
            _dbSet.Remove(enitity);
        }

        public IQueryable<T> GetAll()
        {
            //Track edilmesin ki memeory şişirmesin
            return _dbSet.AsNoTracking().AsQueryable();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            //Track edilmesin ki memeory şişirmesin
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int? id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public void RemoveRange(IEnumerable<T> enitities)
        {
            _dbSet.RemoveRange(enitities);
        }
    }
}
