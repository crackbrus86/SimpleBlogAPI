using AutoMapper;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleBlogAPI.Entities;
using Profile = SimpleBlogAPI.Entities.Profile;

namespace SimpleBlogAPI.Infrastructure
{
    public class ProfileDTOMapper
    {
        public IMapper Mapper;

        public ProfileDTOMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Profile, ProfileDTO>();
                cfg.CreateMap<ProfileDTO, Profile>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
