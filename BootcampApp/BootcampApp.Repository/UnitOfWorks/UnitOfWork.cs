

using BootcampApp.Core.IUnitOfWorks;

namespace BootcampApp.Repository.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BootcampAppDbContext _context;
        public UnitOfWork(BootcampAppDbContext appDbContext)
        {
            _context = appDbContext;

        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();

        }
    }
}
