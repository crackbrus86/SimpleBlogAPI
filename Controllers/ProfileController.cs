using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBlogAPI.Entities;
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
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        private readonly IMapper _mapper;

        public ProfileController(IProfileService profileService)
        {
            _service = profileService;
            _mapper = new ProfileMapper().Mapper;
        }

        [Authorize]
        [HttpGet("getprofile")]
        [Route("/getprofile")]
        public async Task<IActionResult> GetProfile([FromQuery] GetProfileRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });

            var user = (User)Request.HttpContext.Items["User"];
            if(user.ProfileId != contract.Id)
                return BadRequest(new ResponseMessage { Message = "Profile Id is not correct." });

            var profile = await _service.GetProfile(contract.Id, cancellationToken);
            if (profile == null)
                return BadRequest(new ResponseMessage { Message = "Profile not found" });
            return Ok(_mapper.Map<GetProfileResponse>(profile));
        }

        [Authorize]
        [HttpPost("save")]
        [Route("/save")]
        public async Task<IActionResult> Save(SaveProfileRequest contract, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseMessage { Message = "One or more validation errors occurred." });
            var result = await _service.UpdateProfile(_mapper.Map<ProfileDTO>(contract), cancellationToken);

            if (!result)
                return BadRequest(new ResponseMessage { Message = "Profile has not been saved." });

            return Ok(new ResponseMessage { Message = "Profile saved." });
        }
    }
}