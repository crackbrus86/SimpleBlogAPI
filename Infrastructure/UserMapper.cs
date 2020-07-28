using AutoMapper;
using SimpleBlogAPI.Models;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Infrastructure
{
    public class UserMapper
    {
        public IMapper Mapper;

        public UserMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AuthenticateRequest, UserDTO>();
                cfg.CreateMap<AuthenticateDTO, AuthenticateResponse>();
                cfg.CreateMap<RegisterRequest, UserDTO>();
                cfg.CreateMap<ProfileDTO, AuthenticateProfileResponse>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
