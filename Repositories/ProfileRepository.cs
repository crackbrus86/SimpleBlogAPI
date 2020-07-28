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
    public class ProfileRepository : IProfileRepository
    {
        private SimpleBlogContext _context;

        public ProfileRepository(SimpleBlogContext context)
        {
            _context = context;
        }

        public async Task<Profile> GetById(int id, CancellationToken cancellationToken) 
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<int?> Create(Profile profile, CancellationToken cancellationToken)
        {
            try
            {
                await _context.Profiles.AddAsync(profile);
                await _context.SaveChangesAsync();
                return profile.Id;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Update(Profile profile, CancellationToken cancellationToken)
        {
            try
            {
                _context.Entry(profile).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken)
        {
            var profile = await _context.Profiles.FindAsync(id);
            if(profile != null)
            {
                _context.Profiles.Remove(profile);
                return true;
            }
            return false;
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
