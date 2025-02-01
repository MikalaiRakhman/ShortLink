﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ShortLink.DAL.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShortLink.Domain.Entities;

namespace ShortLink.DAL.Identity
{
    public class TokenProvider
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public TokenProvider(IConfiguration configuration, ApplicationDbContext context, UserManager<User> userManager)
        {
            _configuration = configuration;
            _context = context;
            _userManager = userManager;
        }

        public string GenerateJwtToken(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> GenerateRefreshToken(Guid applicationUserId, CancellationToken cancellationToken)
        {
            var refreshTokenValue = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = applicationUserId,
                Token = refreshTokenValue,
                Expires = DateTime.UtcNow.AddDays(1),
            };

            _context.RefreshTokens.Add(refreshToken);

            await _context.SaveChangesAsync(cancellationToken);

            return refreshToken.Token;
        }

        public async Task<(string newJwtToken, string newRefreshToken)> RefreshTokens(string refreshToken, CancellationToken cancellationToken)
        {
            var storedToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken);

            if (storedToken == null || storedToken.Expires < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("The provided refresh token is either invalid or has expired. Please authenticate again to obtain a new token.");
            }

            var applicationUser = await _userManager.Users.FirstOrDefaultAsync(au => au.Id == storedToken.UserId);           

            var roles = await _context.UserRoles
                .Where(ur => ur.UserId == applicationUser.Id)
                .Join(_context.Roles,
                        ur => ur.RoleId,
                        role => role.Id,
                        (ur, role) => role.Name)
                .ToListAsync(cancellationToken);

            var newJwtToken = GenerateJwtToken(applicationUser, roles);
            var newRefreshToken = await GenerateRefreshToken(applicationUser.Id, cancellationToken);

            storedToken.Expires = DateTime.UtcNow;

            _context.RefreshTokens.Update(storedToken);

            await _context.SaveChangesAsync(cancellationToken);

            return (newJwtToken, newRefreshToken);
        }
    }
}
