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
    public class PostDTOMapper
    {
        public IMapper Mapper;

        public PostDTOMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Post, PostDTO>();
                cfg.CreateMap<PostDTO, Post>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<ProfileDTO, Profile>();
                cfg.CreateMap<Profile, ProfileDTO>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
