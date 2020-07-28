using AutoMapper;
using SimpleBlogAPI.Infrastructure;
using SimpleBlogAPI.Interfaces;
using SimpleBlogAPI.Models.DTOs;
using SimpleBlogAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Profile = SimpleBlogAPI.Entities.Profile;

namespace SimpleBlogAPI.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IMapper _mapper;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
            _mapper = new ProfileDTOMapper().Mapper;
        }

        public async Task<ProfileDTO> GetProfile(int id, CancellationToken cancellationToken)
        {
            Profile profile = await _repository.GetById(id, cancellationToken);
            if (profile == null) return null;
            return _mapper.Map<ProfileDTO>(profile);
        }

        public async Task<int?> CreateProfile(ProfileDTO profile, CancellationToken cancellationToken)
        {
            return await _repository.Create(_mapper.Map<Profile>(profile), cancellationToken);
        }

        public async Task<bool> UpdateProfile(ProfileDTO profile, CancellationToken cancellationToken)
        {
            return await _repository.Update(_mapper.Map<Profile>(profile), cancellationToken);
        }
    }
}
