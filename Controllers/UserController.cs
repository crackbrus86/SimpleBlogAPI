using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAPI.Infrastructure;
using SimpleBlogAPI.Interfaces;
using SimpleBlogAPI.Models;
using SimpleBlogAPI.Models.DTOs;
using SimpleBlogAPI.Helpers;
using Microsoft.AspNetCore.Cors;

namespace SimpleBlogAPI.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        private IMapper _mapper;

        public UserController(IUserService userService)
        {
            _userService = userService;
            _mapper = new UserMapper().Mapper;
        }

        [HttpPost("authenticate")]
        [Route("/authenticate")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "One or more validation errors occurred."});
            var response = await _userService.Authenticate(contract.Username, contract.Password, cancellationToken);

            if(response == null)
                return BadRequest(new { message = "User is not found"});

            return Ok(_mapper.Map<AuthenticateResponse>(response));
        }

        [HttpPost("register")]
        [Route("/register")]
        public async Task<IActionResult> Register(RegisterRequest contract, CancellationToken cancellationToken)
        {
            if(!ModelState.IsValid)
                return BadRequest(new { message = "One or more validation errors occurred."});

            var result = await _userService.Register(_mapper.Map<UserDTO>(contract), cancellationToken);
            if(!result)
                return BadRequest(new { message = "Can't create user account."});
            return Ok(new { message = "User has been created!"});
        }

        //[Authorize]
        //[HttpGet("getuserbyid")]
        //[Route("/getuserbyid")]
        //public IActionResult GetUserById()
        //{
        //    var user = (SimpleBlogAPI.Entities.User) HttpContext.Items["User"];
        //    return Ok(user.Id);
        //}
    }
}