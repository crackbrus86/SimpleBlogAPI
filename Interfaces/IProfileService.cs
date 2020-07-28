using AutoMapper;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDTO> GetProfile(int id, CancellationToken cancellationToken);
        Task<int?> CreateProfile(ProfileDTO profile, CancellationToken cancellationToken);
        Task<bool> UpdateProfile(ProfileDTO profile, CancellationToken cancellationToken);
    }
}
