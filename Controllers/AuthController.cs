using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.auth;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public AuthController(UserManager<User> userManager)
        {
            _userManager = userManager;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                // Validate user model
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new User
                {
                    FirstName = registerDto.FirstName,
                    LastName = registerDto.LastName,
                    // PhoneNumber = registerDto.PhoneNumber,
                    UserName = registerDto.Email,
                    Email = registerDto.Email,
                };
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(new { Message = "User created" });
                    }
                    else
                    {
                        return StatusCode(500, new { Message = "Error adding user to role", Details = roleResult.Errors });
                    }
                }
                else
                {
                    return StatusCode(500, new { Message = "Error creating user", Details = createdUser.Errors });
                }
            }
            catch (Exception e)
            {

                return StatusCode(500, new { Message = "An unexpected error occurred. Please try again." });
            }

        }

    }
}