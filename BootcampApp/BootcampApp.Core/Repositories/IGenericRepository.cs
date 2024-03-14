using System.Linq.Expressions;

namespace BootcampApp.Core.Repositories

{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int? id);

        //IQueryable kullanammaızın seebi tamemen performasntır. List kullansak bunu direkt memoryde sql cevirip dbden verileri alacaktı ancak
        //biz bazı durumlarda bu yazdıgımız expressionların (X=>X.Id .... vb) ustune OrderBy() gibi işlemler yapmak istersek dbye atılmadan once memoryde bunlar birleşirilir.
        //Ne zaman ToList() fonk çalışırsa o zaman bu sqller dbden verileri çekecekt ve memorye aktarıalcaktır.
        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);

        void Update(T entity);
        void Remove(T enitity);
        void RemoveRange(IEnumerable<T> enitities);

    }
}
