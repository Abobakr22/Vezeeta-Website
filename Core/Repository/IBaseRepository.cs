﻿using Core.Account_Manager;
using Core.Models;
using System.Linq.Expressions;

namespace Core.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task<ApplicationUser> GetUserAsync(string UserType, string UserName);
        Task<bool> LoginAsync(LoginDto login);

        //method for finding items by criteria (Expression)
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
