using AutoMapper;
using SimpleBlogAPI.Models;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Infrastructure
{
    public class ProfileMapper
    {
        public IMapper Mapper;

        public ProfileMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ProfileDTO, GetProfileResponse>();
                cfg.CreateMap<SaveProfileRequest, ProfileDTO>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
