using AutoMapper;
using SimpleBlogAPI.Models;
using SimpleBlogAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Infrastructure
{
    public class PostMapper
    {
        public IMapper Mapper;

        public PostMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<PostDTO, GetAllResponse>();
                cfg.CreateMap<PostDTO, GetAllMyPostsResponse>();
                cfg.CreateMap<PostDTO, GetPostResponse>();
                cfg.CreateMap<PostDTO, GetMyPostResponse>();
                cfg.CreateMap<CreatePostRequest, PostDTO>();
            });
            Mapper = config.CreateMapper();
        }
    }
}
