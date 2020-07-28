using Microsoft.EntityFrameworkCore;
using SimpleBlogAPI.Context;
using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private SimpleBlogContext _context;

        public UserRepository(SimpleBlogContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string username, string password, CancellationToken cancellationToken = default)
        {
            return await _context.Users.Where(u => u.Username == username && u.Password == password).Include(u => u.Profile).FirstOrDefaultAsync();
        }

        public async Task<bool> Insert(User user, CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }catch
            {
                return false;
            }

        }

        public async Task<User> Find(Expression<Func<User, Boolean>> predicate)
        {
            return await _context.Users.SingleOrDefaultAsync(predicate);
        }

        public User GetUserById(int id)
        {
            return _context.Users.Find(id);
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
