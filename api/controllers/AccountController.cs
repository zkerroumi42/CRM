using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.interfaces;
using api.Interfaces;
using api.models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signinManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager,IEmailService emailService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signinManager = signInManager;
            _emailService=emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");

            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                UserName = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // Remove the JWT token from local storage client
        // localStorage.removeItem("token");
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return Ok(new { message = "Logged out successfully" });
        }

        
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDto forgetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(forgetPasswordDto.Email);
            if (user == null) return NotFound("User not found");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            
             await _emailService.SendPasswordResetEmail(user.Email, token);

            return Ok(new { message = "Password reset token sent to email", token });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.        NewPassword);

            if (result.Succeeded)
                return Ok(new { message = "Password reset successful" });

            return BadRequest(result.Errors);
        }

        [HttpGet("signin-google")]
        public IActionResult SignInWithGoogle()
        {
            // This will redirect the user to Google's OAuth 2.0 consent screen
            var properties = new AuthenticationProperties
            {
                RedirectUri = "http://localhost:5257"
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("signin-google-callback")]
public async Task<IActionResult> SignInGoogleCallback()
{
    var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
    if (!result.Succeeded)
    {
        return Unauthorized();
    }

    var claims = result.Principal?.Identities?.FirstOrDefault()?.Claims;
    var userId = claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

    if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(email))
    {
        return BadRequest("User information could not be retrieved.");
    }

    // Find the user by email
    var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email);
    
    if (user == null)
    {
        // Optionally, you can create a new user here if the user does not exist in your database
        user = new AppUser
        {
            UserName = email.Split('@')[0], // Use the part before the @ as the username
            Email = email
        };
        var createResult = await _userManager.CreateAsync(user);
        if (!createResult.Succeeded)
        {
            return StatusCode(500, createResult.Errors);
        }
    }

    // Generate a JWT token using your existing CreateToken method
    var token = _tokenService.CreateToken(user);

    return Ok(new { Token = token, Email = email });
}




    }
}