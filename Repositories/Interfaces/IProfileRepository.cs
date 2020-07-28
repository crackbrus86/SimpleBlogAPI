using SimpleBlogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Repositories.Interfaces
{
    public interface IProfileRepository : IDisposable
    {
        Task<Profile> GetById(int id, CancellationToken cancellationToken);
        Task<int?> Create(Profile profile, CancellationToken cancellationToken);
        Task<bool> Update(Profile profile, CancellationToken cancellationToken);
        Task<bool> Delete(int id, CancellationToken cancellationToken);
    }
}
