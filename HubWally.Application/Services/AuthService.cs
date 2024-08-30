using HubWally.Application.DTOs.Accounts; 
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;  
using System.Text;
using HubWally.Application.Services.IServices;
using Microsoft.AspNetCore.Identity; 
using Microsoft.EntityFrameworkCore;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;
using Microsoft.Extensions.Configuration;

namespace HubWally.Application.Services
{
    public class AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration) : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly SignInManager<IdentityUser> _signInManager = signInManager;
        private readonly IConfiguration _configuration = configuration;

        public async Task<IdentityResult> Register(RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.PhoneNumber,
                PhoneNumber = model.PhoneNumber
            };

            return await _userManager.CreateAsync(user, model.Password);
        }
        public async Task<string?> Login(LoginDto model)
        {
            // Find the user by phone number
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);

            if (user != null)
            {
                // Attempt to sign in the user
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                if (result.Succeeded)
                {
                    return GenerateJwtToken(user);
                }
            }
            return null;
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }; 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
