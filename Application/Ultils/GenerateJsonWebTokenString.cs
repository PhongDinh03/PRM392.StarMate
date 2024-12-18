﻿using Application.Commons;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Ultils
{
    public static class GenerateJsonWebTokenString
    {
        public static string GenerateJsonWebToken(this User user, AppConfiguration appSettingConfiguration, string secretKey, DateTime now)
        {
            if (Encoding.UTF8.GetBytes(secretKey).Length < 32)
            {
                // Adjust key length to 32 bytes (256 bits) using padding if necessary
                secretKey = secretKey.PadRight(32, '0');
            }
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("Email" ,user.Email),
                new Claim(ClaimTypes.Role ,user.RoleName),
            };
            var token = new JwtSecurityToken(
                issuer: appSettingConfiguration.JWTSection.Issuer,
                audience: appSettingConfiguration.JWTSection.Audience,
                claims: claims,
                expires: now.AddHours(3),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
