using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.auth
{
    public class RegisterDto
    // create new user DTO
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]

        public string? PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}