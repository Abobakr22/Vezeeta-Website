using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Account_Manager;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Data.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext context;

        public BaseRepository(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public BaseRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> LoginAsync(LoginDto login)
        {
            return (await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false)).Succeeded;
        }

        public async Task<ApplicationUser> GetUserAsync(string UserType, string UserName)
        {
            return (await _userManager.GetUsersInRoleAsync(UserType)).FirstOrDefault(x => x.UserName == UserName);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);

        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        //public async Task AddRangeAsync(IEnumerable<T> entities)
        //{
        //    await _context.Set<T>().AddRangeAsync(entities);
        //    await _context.SaveChangesAsync();
        //}

        public Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().SingleOrDefaultAsync(predicate);
        }
        async Task<T> IBaseRepository<T>.FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().FindAsync(predicate);
        }
        public async Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        //public void DeleteRange(IEnumerable<T> entities)
        //{
        //    _context.Set<T>().RemoveRange(entities);
        //}


        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)
        {
            return await _context.Set<T>().CountAsync(criteria);
        }
    }
}
