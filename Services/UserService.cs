using AutoMapper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleBlogAPI.Entities;
using SimpleBlogAPI.Helpers;
using SimpleBlogAPI.Infrastructure;
using SimpleBlogAPI.Interfaces;
using SimpleBlogAPI.Models.DTOs;
using SimpleBlogAPI.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IProfileService _profileService;

        public UserService(IUserRepository repository, IOptions<AppSettings> appSettings, IProfileService profileService)
        {
            _repository = repository;
            _mapper = new UserDTOMapper().Mapper;
            _appSettings = appSettings.Value;
            _profileService = profileService;
        }

        public async Task<AuthenticateDTO> Authenticate(string username, string password, CancellationToken cancellationToken = default)
        {
            User user = await _repository.GetUser(username, hashPassword(password), cancellationToken);
            if (user == null) return null;
            UserDTO userDto = _mapper.Map<UserDTO>(user);
            var token = generateJwtToken(userDto);
            return new AuthenticateDTO(userDto, token);
        }

        public async Task<bool> Register(UserDTO user, CancellationToken cancellationToken = default)
        {
            var sameUser = await _repository.Find(x => x.Username == user.Username || x.Email == user.Email);
            if(sameUser != null)
                return false;
            user.Password = hashPassword(user.Password);
            int? profileId = await _profileService.CreateProfile(new ProfileDTO { FirstName = user.FirstName, LastName = user.LastName }, cancellationToken);
            if (profileId == null) return false;
            user.ProfileId = profileId;
            return await _repository.Insert(_mapper.Map<User>(user), cancellationToken);
        }

        public User GetUserById(int id)
        {
            return _repository.GetUserById(id);
        }

        private string generateJwtToken(UserDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string hashPassword(string password)
        {
            var salt = Encoding.ASCII.GetBytes(_appSettings.Salt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA1,
                    iterationCount: 10000,
                    numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
