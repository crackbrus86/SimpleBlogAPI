using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAll(CancellationToken cancellationToken);
        Task<IEnumerable<PostDTO>> GetAllByUserId(int userId, CancellationToken cancellationToken);
        Task<PostDTO> Get(int postId, CancellationToken cancellationToken);
        Task<bool> Create(PostDTO post, int userId, CancellationToken cancellationToken);
        Task<bool> Update(PostDTO post, CancellationToken cancellationToken);
        Task<bool> Publish(int postId, CancellationToken cancellationToken);
        Task<bool> Delete(int postId, CancellationToken cancellationToken);
    }
}
