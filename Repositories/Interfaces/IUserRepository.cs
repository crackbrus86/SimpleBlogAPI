using SimpleBlogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Repositories.Interfaces
{
    public interface IUserRepository : IDisposable
    {
        Task<User> GetUser(string username, string password, CancellationToken cancellationToken);
        Task<bool> Insert(User user, CancellationToken cancellationToken);
        User GetUserById(int id);
        Task<User> Find(Expression<Func<User, Boolean>> predicate);
    }
}
