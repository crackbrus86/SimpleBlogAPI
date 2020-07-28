using Microsoft.EntityFrameworkCore;
using SimpleBlogAPI.Context;
using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private SimpleBlogContext _context;

        public PostRepository(SimpleBlogContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Post>> GetAll(CancellationToken cancellationToken)
        {
            return await _context.Posts.Where(p => p.IsPublished).ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        {
            return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<Post> Get(int postId, CancellationToken cancellationToken)
        {
            return await _context.Posts.Include(p => p.User).ThenInclude(u => u.Profile).FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<bool>  Create(Post post, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(Post post, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Publish(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(postId);
            if (post != null)
            {
                post.IsPublished = true;
                post.PublishedDate = DateTime.Now;
                _context.Entry(post).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(int postId, CancellationToken cancellationToken)
        {
            var post = await _context.Posts.FindAsync(postId);
            if(post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        private bool _disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
