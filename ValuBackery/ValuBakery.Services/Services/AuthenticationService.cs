using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ValuBakery.Application.Services.Interfaces;
using ValuBakery.Data.DTOs;
using ValuBakery.Data.DTOs.Authorization;

namespace ValuBakery.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly IClaimsService _claimsService;

        public AuthenticationService(IUserService userService, IConfiguration config, IClaimsService claimsService)
        {
            _userService = userService;
            _config = config;
            _claimsService = claimsService;
        }

        public async Task<TokenDto?> LoginProccess(LoginDto login)
        {
            var userDto = await _userService.GetUserByCredentialsAsync(login.UserName, login.Password);

            if (userDto == null) return null;

            var token = Generate(userDto);

            userDto = await GenerateRefreshToken(userDto);

            return new TokenDto()
            {
                UserId = userDto.Id,
                Token = token,
                RefreshToken = userDto.RefreshToken
            };
        }

        public async Task<UserDto> GenerateRefreshToken(UserDto userDto)
        {
            userDto.RefreshToken = GenerateRefreshToken();
            userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);

            await _userService.UpdateByLoginAsync(userDto);

            return userDto;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public string Generate(UserDto userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Crear los claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userDto.UserName),
                new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            };


            // Crear el token
            //In Prod set expires: DateTime.UtcNow.AddDays(2),
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(12),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<TokenDto> GetRefreshTokenAsync(TokenDto tokenDto)
        {
            var userId = (int)_claimsService.Claims?.UserId;

            var userDto = await _userService.GetByIdAsync(userId);

            if (userDto is null)
            {
                return null;
            }

            if (userDto.RefreshToken != tokenDto.RefreshToken || userDto.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return null;
            }

            userDto.RefreshToken = GenerateRefreshToken();
            userDto.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(30);

            await _userService.UpdateAsync(userDto);

            TokenDto newToken = new()
            {
                Token = Generate(userDto),
                RefreshToken = userDto.RefreshToken,
                RefreshTokenExpiryTime = (DateTime)userDto.RefreshTokenExpiryTime
            };

            return newToken;
        }
    }
}
