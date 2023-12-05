using Core.Account_Manager;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        
        Task<bool> Login (LoginDto login);

        //method for finding items by criteria (Expression)
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        //Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);
        
        Task UpdateAsync (T entity);
        Task DeleteAsync (int id);
        //Task DeleteRangeAsync(IEnumerable<T> entities); 
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria); 
    }
}
