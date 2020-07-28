using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBlogAPI.Models
{
    public class GetProfileRequest
    {
        [Required]
        public int Id { get; set; }
    }
    public class GetProfileResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
    }

    public class SaveProfileRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
    }
}
