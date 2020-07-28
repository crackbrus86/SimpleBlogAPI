using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Models.DTOs
{
    public class AuthenticateDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public ProfileDTO? Profile {get;set;}

        public AuthenticateDTO(UserDTO user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Token = token;
            Profile = user.Profile;
        }
    }
}
