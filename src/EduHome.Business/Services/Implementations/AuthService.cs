using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
		{
			_userManager = userManager;
			_configuration = configuration;
		}



		public async Task Register(RegisterDTO registerDTO)
		{
			AppUser user = new()
			{
				Fullname = registerDTO.Fullname,
				UserName = registerDTO.Username,
				Email = registerDTO.Email
			};


			var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);
			if (!identityResult.Succeeded)
			{
				string errors = string.Empty;
				int count = 0;
				foreach (var error in identityResult.Errors)
				{
					errors += count != 0 ? $",{error.Description}" : $"{error.Description}";
					count++;
				}
				throw new UserCreatFailException(errors);
			}

			var result = await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
			if (!result.Succeeded)
			{
				string errors = string.Empty;
				int count = 0;
				foreach (var error in result.Errors)
				{
					errors += count != 0 ? $",{error.Description}" : $"{error.Description}";
					count++;
				}
				throw new RoleCreateFailException(errors);
			}
		}
		public async Task<TokenResponseDTO> Login(LoginDTO loginDTO)
		{
			var user = await _userManager.FindByNameAsync(loginDTO.Username);
			if (user is null)
				throw new AuthFailException("Username or password is incorrect");
			var check = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
			if (!check)
				throw new AuthFailException("Username or password is incorrect");

			//create JWT


			List<Claim> claims = new()
			{
			new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
			new Claim(ClaimTypes.Email, user.Email)
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt: SecurityKey"])); //byte arrayine cevirme
			SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

			JwtSecurityToken jwtSecurityToken = new(
			issuer: _configuration["Jwt: Issuer"],
			audience: _configuration["Jwt: Audience"],
			claims: claims,
			notBefore: DateTime.UtcNow,
			expires: DateTime.UtcNow.AddMinutes(1),
			signingCredentials: signingCredentials
			);
			//JwtSecurityToken obyektden stringe cevirme
			JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
			string token=jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

			return new TokenResponseDTO()
			{ 
				Token = token,
				Expires= jwtSecurityToken.ValidTo,
				Username=user.UserName
			};

		}

	}
}
