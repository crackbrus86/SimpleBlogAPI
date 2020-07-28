using SimpleBlogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Repositories.Interfaces
{
    public interface IPostRepository : IDisposable
    {
        Task<IEnumerable<Post>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<Post>> GetAllByUserId(int userId, CancellationToken cancellationToken);
        Task<Post> Get(int postId, CancellationToken cancellationToken);
        Task<bool> Create(Post post, CancellationToken cancellationToken);
        Task<bool> Update(Post post, CancellationToken cancellationToken);
        Task<bool> Publish(int postId, CancellationToken cancellationToken);
        Task<bool> Delete(int postId, CancellationToken cancellationToken);
    }
}
