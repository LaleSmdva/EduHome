using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
	public class AuthService:IAuthService
	{
		private readonly UserManager<AppUser> _userManager;

		public AuthService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task Register(RegisterDTO registerDTO)
		{
			AppUser user = new()
			{
				Fullname= registerDTO.Fullname,
				UserName= registerDTO.Username,
				Email= registerDTO.Email
			};


			var identityResult=await _userManager.CreateAsync(user,registerDTO.Password);
			if(!identityResult.Succeeded)
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

			var result=await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
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
	}
}
