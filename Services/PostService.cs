using AutoMapper;
using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Infrastructure;
using SimpleBlogAPI.Interfaces;
using SimpleBlogAPI.Models.DTOs;
using SimpleBlogAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IUserService userService)
        {
            _repository = postRepository;
            _userService = userService;
            _mapper = new PostDTOMapper().Mapper;
        }

        public async Task<IEnumerable<PostDTO>> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<Post> posts = await _repository.GetAll(cancellationToken);
            return posts.Select(p => _mapper.Map<PostDTO>(p));
        }

        public async Task<IEnumerable<PostDTO>> GetAllByUserId(int userId, CancellationToken cancellationToken)
        {
            IEnumerable<Post> posts = await _repository.GetAllByUserId(userId, cancellationToken);
            return posts.Select(p => _mapper.Map<PostDTO>(p));
        }

        public async Task<PostDTO> Get(int postId, CancellationToken cancellationToken)
        {
            Post post = await _repository.Get(postId, cancellationToken);
            PostDTO result = _mapper.Map<PostDTO>(post);
            result.Author = (result.User?.Profile?.FirstName?.Length > 0 && result.User?.Profile?.LastName?.Length > 0) ? String.Format("{0} {1}", result.User.Profile.FirstName, result.User.Profile.LastName) : result.User.Username;
            return result;
        }

        public async Task<bool> Create(PostDTO post, int userId, CancellationToken cancellationToken)
        {
            User user = _userService.GetUserById(userId);
            if(user != null)
            {
                post.UserId = user.Id;
                post.CreatedDate = DateTime.Now;
                post.IsPublished = false;                
                return await _repository.Create(_mapper.Map<Post>(post), cancellationToken);
            }
            return false;
        }

        public async Task<bool> Update(PostDTO post, CancellationToken cancellationToken)
        {
            post.UpdatedDate = DateTime.Now;
            return await _repository.Update(_mapper.Map<Post>(post), cancellationToken);
        }

        public async Task<bool> Publish(int postId, CancellationToken cancellationToken)
        {
            return await _repository.Publish(postId, cancellationToken);
        }

        public async Task<bool> Delete(int postId, CancellationToken cancellationToken)
        {
            return await _repository.Delete(postId, cancellationToken);
        }
    }
}
