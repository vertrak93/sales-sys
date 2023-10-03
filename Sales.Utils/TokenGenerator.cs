﻿using Microsoft.IdentityModel.Tokens;
using Sales.DTOs;
using Sales.DTOs.UserDtos;
using Sales.Models;
using Sales.Utils.Constants;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Utils
{
    
    public class TokenGenerator
    {

        private static TokenGenerator _instance = null;

        public static TokenGenerator Instance()
        {
            if(_instance == null) _instance= new TokenGenerator();
            return _instance;
        }

        private TokenGenerator() { }

        public string GenerateJWTToken(UserDto user, List<RoleDto> roles, string keyJwt)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwt));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var secuityToken = new JwtSecurityToken(
                                claims: claims,
                                expires: DateTime.Now.AddMinutes(60),
                                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(secuityToken);

            return jwt;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetClaimsPrincipalExpiredToken(string token, string keyJwt)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyJwt)),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException(Messages.InvalidToken);

            return principal;
        }
    }
}