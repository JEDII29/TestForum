using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.Data.Entities;

namespace TestForum.API.Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<UserEntity> _userManager;
		public AuthenticationService(UserManager<UserEntity> userManager) {
			_userManager = userManager;
		}

		public async Task<string> GenerateAccessToken(UserEntity user)
		{
			var rsaKey = RSA.Create();
			rsaKey.ImportRSAPrivateKey(File.ReadAllBytes("key"), out _);
			var key = new RsaSecurityKey(rsaKey);
			var handler = new JsonWebTokenHandler();
			var roles = await _userManager.GetRolesAsync(user);
			var claims = new List<Claim>
			{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // Id użytkownika
					new Claim(ClaimTypes.Name, user.UserName),
			};
			foreach (var role in roles) 
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}
			var token = handler.CreateToken(new SecurityTokenDescriptor()
			{
				Issuer = "http://localhost:5063",
				Subject = new ClaimsIdentity(claims),
				SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),
				Expires = DateTime.UtcNow.AddMinutes(10)
			});

			return token;
		}
	}
}
