﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebWorker.Data.Entities.Identity;

namespace WebWorker.BLL.Managers.JwtToken
{
    public class JwtTokenManager(UserManager<UserEntity> userManager,
        IConfiguration configuration) : IJwtTokenManager
    {
        public async Task<string> CrateJwtTokenAsync(UserEntity user)
        {
            string key = configuration["JWT:Key"]!;

            var claims = new List<Claim>
        {
            new Claim("email", user.Email ?? ""),
            new Claim("name", $"{user.FirstName} {user.LastName}"),
            new Claim("image", user.Image != null? user.Image : "")
        };

            foreach (var role in await userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim("role", role));
            }

            var keyBytes = System.Text.Encoding.UTF8.GetBytes(key);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            var sec = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(2),
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(sec);

            return token;
        }
    }
}
