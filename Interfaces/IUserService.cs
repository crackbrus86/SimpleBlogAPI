using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Interfaces
{
    public interface IUserService
    {
        Task<bool> Register(UserDTO user, CancellationToken cancellationToken);
        Task<AuthenticateDTO> Authenticate(string username, string password, CancellationToken cancellationToken);
        User GetUserById(int id);
    }
}
