using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAPI.Helpers;
using SimpleBlogAPI.Infrastructure;
using SimpleBlogAPI.Interfaces;
using SimpleBlogAPI.Models;
using SimpleBlogAPI.Models.DTOs;

namespace SimpleBlogAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService)
        {
            _postService = postService;
            _mapper = new PostMapper().Mapper;
        }

        [HttpGet("getall")]
        [Route("/getall")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<PostDTO> posts = await _postService.GetAll(cancellationToken);
            return Ok(posts.Select(p => _mapper.Map<GetAllResponse>(p)));
        }

        [Authorize]
        [HttpGet("getmyposts")]
        [Route("/getmyposts")]
        public async Task<IActionResult> GetMyPosts(CancellationToken cancellationToken)
        {
            var user = (SimpleBlogAPI.Entities.User)HttpContext.Items["User"];
            IEnumerable<PostDTO> myPosts = await _postService.GetAllByUserId(user.Id, cancellationToken);
            return Ok(myPosts.Select(p => _mapper.Map<GetAllMyPostsResponse>(p)));
        }

        [HttpGet("getpost")]
        [Route("/getpost")]
        public async Task<IActionResult> GetPost([FromQuery] GetPostRequest contract, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });

            PostDTO post = await _postService.Get(contract.Id, cancellationToken);

            if (post == null)
                return BadRequest(new ResponseMessage { Message = "Post not found." });
            return Ok(_mapper.Map<GetPostResponse>(post));
        }

        [Authorize]
        [HttpGet("getmypost")]
        [Route("/getmypost")]
        public async Task<IActionResult> GetMyPost([FromQuery] GetMyPostRequest contract, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });

            PostDTO post = await _postService.Get(contract.Id, cancellationToken);

            if (post == null)
                return BadRequest(new ResponseMessage { Message = "Post not found." });
            return Ok(_mapper.Map<GetMyPostResponse>(post));
        }

        [Authorize]
        [HttpPost("create")]
        [Route("/create")]
        public async Task<IActionResult> Create(CreatePostRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });
            var user = (SimpleBlogAPI.Entities.User)HttpContext.Items["User"];
            bool result = await _postService.Create(_mapper.Map<PostDTO>(contract), user.Id, cancellationToken);
            if (!result)
                return BadRequest(new ResponseMessage { Message = "Post has't been created. Something went wrong." });
            return Ok(new ResponseMessage { Message = "Post has been created." });
        }

        [Authorize]
        [HttpPost("update")]
        [Route("/update")]
        public async Task<IActionResult> Update(UpdatePostRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });

            var user = (SimpleBlogAPI.Entities.User)HttpContext.Items["User"];
            var post = _mapper.Map<PostDTO>(contract);
            post.UserId = user.Id;
            bool result = await _postService.Update(post, cancellationToken);
            if (!result)
                return BadRequest(new ResponseMessage { Message = "Post hasn't been updated." });
            return Ok(new ResponseMessage { Message = "Post has been updated." });
        }

        [Authorize]
        [HttpPost("publish")]
        [Route("/publish")]
        public async Task<IActionResult> Publish(PublishRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occured." });

            bool result = await _postService.Publish(contract.Id, cancellationToken);
            if (!result)
                return BadRequest(new ResponseMessage { Message = "Publish failed." });
            return Ok(new ResponseMessage { Message = "Publish succeeded." });
        }

        [Authorize]
        [HttpPost("delete")]
        [Route("/delete")]
        public async Task<IActionResult> Delete(DeleteRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occured." });

            bool result = await _postService.Delete(contract.Id, cancellationToken);
            if (!result)
                return BadRequest(new ResponseMessage { Message = "Delete failed." });
            return Ok(new ResponseMessage { Message = "Post has been deleted." });
        }
    }
}