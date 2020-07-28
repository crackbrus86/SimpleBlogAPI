using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Models
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class AuthenticateResponse
    {
        public string Token { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public AuthenticateProfileResponse Profile { get; set; }
    }

    public class AuthenticateProfileResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public class RegisterRequest 
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Password {get;set;}
        [Compare("Password")]
        public string Confirm {get;set;}
    }
}
