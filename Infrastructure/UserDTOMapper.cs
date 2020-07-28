using AutoMapper;
using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Profile = SimpleBlogAPI.Entities.Profile;

namespace SimpleBlogAPI.Infrastructure
{
    public class UserDTOMapper
    {
        public IMapper Mapper;

        public UserDTOMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Profile, ProfileDTO>();
                cfg.CreateMap<ProfileDTO, Profile>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
 