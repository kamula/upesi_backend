using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.auth;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ItokenService _tokenService;

        private readonly SignInManager<User> _signInManager;
        public AuthController(UserManager<User> userManager, ItokenService tokenService, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // Check if user's exists in DB
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            // If user's email does not exist in DB return unauthorized
            if (user == null) return Unauthorized("Invalid username");
            // If it exists validate the password

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found/Password incorrect");

            return Ok(
                new NewUserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _tokenService.CreateToken(user)
                }
            );

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

        // [HttpPost("login")]

    }
}